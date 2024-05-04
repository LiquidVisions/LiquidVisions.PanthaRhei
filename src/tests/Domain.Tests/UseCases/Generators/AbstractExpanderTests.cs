using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Tests.Mocks;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.PostProcessors;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Preprocessors;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Rejuvenators;
using LiquidVisions.PanthaRhei.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Domain.Tests.UseCases.Generators
{
    /// <summary>
    /// Tests for the <see cref="AbstractExpander{TExpander}"/> class.
    /// </summary>
    public class AbstractExpanderTests
    {
        private readonly Fakes fakes = new();
        private readonly FakeAbstractExpander expander;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractExpanderTests"/> class.
        /// </summary>
        public AbstractExpanderTests()
        {
            App app = new()
            {
                Expanders = new List<Expander>()
                {
                    new()
                    {
                        Name = "FakeAbstract",
                        Order = 1,
                    }
                }
            };

            fakes.IDependencyFactory.Setup(x => x.Resolve<App>()).Returns(app);
            expander = new(fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Tests that the constructor verifies dependencies.
        /// </summary>
        [Fact]
        public void ConstructorShouldVerifyDependencies()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Resolve<App>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<ILogger>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(2));
        }

        /// <summary>
        /// Test that should throw ArgumentNullException.
        /// </summary>
        [Fact]
        public void ShouldThrowArgumentNullException()
        {
            // arrange
            // act
            // assert
            Assert.Throws<ArgumentNullException>(() => new FakeAbstractExpander(null)); 
        }

        /// <summary>
        /// Tests that the Name property is set correctly.
        /// </summary>
        [Fact]
        public void NameShouldBeEqualToTheExpanderNameWithoutTheExpanderKeyword()
        {
            // arrange
            // act
            // assert
            Assert.Equal(nameof(FakeAbstractExpander).Replace("Expander", string.Empty, StringComparison.InvariantCulture), expander.Name);
        }

        /// <summary>
        /// Tests the Order property is set correctly.  
        /// </summary>
        [Fact]
        public void EnsuringTheOrderIsSetCorrectlyDependingOnTheConstructorTypeUsed()
        {
            // arrange
            FakeAbstractExpander sub = new();
            // act
            // assert
            Assert.Equal(1, expander.Order);
            Assert.Equal(12, sub.Order);
        }

        /// <summary>
        /// Verifies that the <see cref="AbstractExpander{TExpander}.GetTasks"/> method resolves all <see cref="IExpanderTask{TExpander}"/>.
        /// </summary>
        [Fact]
        public void VerifyThatExpanderTasksAreResolved()
        {
            // arrange
            // act
            expander.GetTasks();

            // assert
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<IExpanderTask<FakeAbstractExpander>>(), Times.Once);
        }

        /// <summary>
        /// Verifies that the <see cref="AbstractExpander{TExpander}.GetHarvesters"/> method resolves all <see cref="IHarvester{TExpander}"/>.
        /// </summary>
        [Fact]
        public void VerifyThatExpanderHarvestersAreResolved()
        {
            // arrange
            // act
            expander.GetHarvesters();

            // assert
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<IHarvester<FakeAbstractExpander>>(), Times.Once);
        }

        /// <summary>
        /// Verifies that the <see cref="AbstractExpander{TExpander}.GetRejuvenators"/> method resolves all <see cref="IHarvester{TExpander}"/>.
        /// </summary>
        [Fact]
        public void VerifyThatExpanderRejuvenatorsAreResolved()
        {
            // arrange
            // act
            expander.GetRejuvenators();

            // assert
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<IRejuvenator<FakeAbstractExpander>>(), Times.Once);
        }

        /// <summary>
        /// Verifies that the <see cref="AbstractExpander{TExpander}.GetPreProcessor"/> method resolves all <see cref="IPreProcessor{TExpander}"/>.
        /// </summary>
        [Fact]
        public void VerifyThatExpanderPreProcessorsAreResolved()
        {
            // arrange
            // act
            expander.GetPreProcessor();

            // assert
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<IPreProcessor<FakeAbstractExpander>>(), Times.Once);
        }

        /// <summary>
        /// Verifies that the <see cref="AbstractExpander{TExpander}.GetPostProcessor"/> method resolves all <see cref="IPostProcessor{TExpander}"/>.
        /// </summary>
        [Fact]
        public void VerifyThatExpanderPostProcessorsAreResolved()
        {
            // arrange
            // act
            expander.GetPostProcessor();

            // assert
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<IPostProcessor<FakeAbstractExpander>>(), Times.Once);
        }

        /// <summary>
        /// Test that verifies that the <see cref="AbstractExpander{TExpander}.Expand"/> method executes all <see cref="IExpanderTask{TExpander}"/> in the correct order.
        /// </summary>
        [Fact]
        public void VerifyExecutionOfExpanderTasksInStrictSequenceOrder()
        {
            // arrange
            MockSequence sequence = new();
            Mock<IExpanderTask<FakeAbstractExpander>> task1 = new(MockBehavior.Strict);
            task1.Setup(x => x.Order).Returns(1);
            task1.Setup(x => x.Enabled).Returns(true);
            task1.InSequence(sequence).Setup(x => x.Execute());

            Mock<IExpanderTask<FakeAbstractExpander>> task2 = new(MockBehavior.Strict);
            task2.Setup(x => x.Order).Returns(2);
            task2.Setup(x => x.Enabled).Returns(true);
            task2.InSequence(sequence).Setup(x => x.Execute());

            Mock<IExpanderTask<FakeAbstractExpander>> task3 = new(MockBehavior.Strict);
            task3.Setup(x => x.Order).Returns(3);
            task3.Setup(x => x.Enabled).Returns(true);
            task3.InSequence(sequence).Setup(x => x.Execute());

            Mock<IExpanderTask<FakeAbstractExpander>> task4 = new(MockBehavior.Strict);
            task4.Setup(x => x.Order).Returns(4);
            task4.Setup(x => x.Enabled).Returns(false);
            task4.InSequence(sequence).Setup(x => x.Execute());

            fakes.IDependencyFactory.Setup(x => x.ResolveAll<IExpanderTask<FakeAbstractExpander>>()).Returns([task1.Object, task2.Object, task3.Object, task4.Object]);

            // act
            expander.Expand();

            // assert
            task1.Verify(x => x.Execute(), Times.Once);
            task2.Verify(x => x.Execute(), Times.Once);
            task3.Verify(x => x.Execute(), Times.Once);
            task4.Verify(x => x.Execute(), Times.Never);
        }

        /// <summary>
        /// Test that verifies that the <see cref="AbstractExpander{TExpander}.Harvest"/> method executes all <see cref="IHarvester{TExpander}"/> in the correct order.
        /// </summary>
        [Fact]
        public void VerifyExecutionOfHarvestersInStrictSequenceOrder()
        {
            // arrange
            MockSequence sequence = new();
            Mock<IHarvester<FakeAbstractExpander>> harvester1 = new(MockBehavior.Strict);
            harvester1.Setup(x => x.Enabled).Returns(true);
            harvester1.InSequence(sequence).Setup(x => x.Execute());

            Mock<IHarvester<FakeAbstractExpander>> harvester2 = new(MockBehavior.Strict);
            harvester2.Setup(x => x.Enabled).Returns(true);
            harvester2.InSequence(sequence).Setup(x => x.Execute());

            Mock<IHarvester<FakeAbstractExpander>> harvester3 = new(MockBehavior.Strict);
            harvester3.Setup(x => x.Enabled).Returns(true);
            harvester3.InSequence(sequence).Setup(x => x.Execute());

            Mock<IHarvester<FakeAbstractExpander>> harvester4 = new(MockBehavior.Strict);
            harvester4.Setup(x => x.Enabled).Returns(false);
            harvester4.InSequence(sequence).Setup(x => x.Execute());

            fakes.IDependencyFactory.Setup(x => x.ResolveAll<IHarvester<FakeAbstractExpander>>()).Returns([harvester1.Object, harvester2.Object, harvester3.Object, harvester4.Object]);

            // act
            expander.Harvest();

            // assert
            harvester1.Verify(x => x.Execute(), Times.Once);
            harvester2.Verify(x => x.Execute(), Times.Once);
            harvester3.Verify(x => x.Execute(), Times.Once);
            harvester4.Verify(x => x.Execute(), Times.Never);
        }

        /// <summary>
        /// Test that verifies that the <see cref="AbstractExpander{TExpander}.Rejuvenate"/> method executes all <see cref="IRejuvenator{TExpander}"/> in the correct order.
        /// </summary>
        [Fact]
        public void VerifyExecutionOfRejuvenatorsInStrictSequenceOrder()
        {
            // arrange
            MockSequence sequence = new();
            Mock<IRejuvenator<FakeAbstractExpander>> rejuvenator1 = new(MockBehavior.Strict);
            rejuvenator1.Setup(x => x.Enabled).Returns(true);
            rejuvenator1.InSequence(sequence).Setup(x => x.Execute());

            Mock<IRejuvenator<FakeAbstractExpander>> rejuvenator2 = new(MockBehavior.Strict);
            rejuvenator2.Setup(x => x.Enabled).Returns(true);
            rejuvenator2.InSequence(sequence).Setup(x => x.Execute());

            Mock<IRejuvenator<FakeAbstractExpander>> rejuvenator3 = new(MockBehavior.Strict);
            rejuvenator3.Setup(x => x.Enabled).Returns(true);
            rejuvenator3.InSequence(sequence).Setup(x => x.Execute());

            Mock<IRejuvenator<FakeAbstractExpander>> rejuvenator4 = new(MockBehavior.Strict);
            rejuvenator4.Setup(x => x.Enabled).Returns(false);
            rejuvenator4.InSequence(sequence).Setup(x => x.Execute());

            fakes.IDependencyFactory.Setup(x => x.ResolveAll<IRejuvenator<FakeAbstractExpander>>()).Returns([rejuvenator1.Object, rejuvenator2.Object, rejuvenator3.Object, rejuvenator4.Object]);

            // act
            expander.Rejuvenate();

            // assert
            rejuvenator1.Verify(x => x.Execute(), Times.Once);
            rejuvenator2.Verify(x => x.Execute(), Times.Once);
            rejuvenator3.Verify(x => x.Execute(), Times.Once);
            rejuvenator4.Verify(x => x.Execute(), Times.Never);
        }

        /// <summary>
        /// Test that verifies that the <see cref="AbstractExpander{TExpander}.PostProcess"/> method executes all <see cref="IPostProcessor{TExpander}"/> in the correct order.
        /// </summary>
        [Fact]
        public void VerifyExecutionOfPostProcessorInStrictSequenceOrder()
        {
            // arrange
            MockSequence sequence = new();
            Mock<IPostProcessor<FakeAbstractExpander>> processor1 = new(MockBehavior.Strict);
            processor1.Setup(x => x.Enabled).Returns(true);
            processor1.InSequence(sequence).Setup(x => x.Execute());

            Mock<IPostProcessor<FakeAbstractExpander>> processor2 = new(MockBehavior.Strict);
            processor2.Setup(x => x.Enabled).Returns(true);
            processor2.InSequence(sequence).Setup(x => x.Execute());

            Mock<IPostProcessor<FakeAbstractExpander>> processor3 = new(MockBehavior.Strict);
            processor3.Setup(x => x.Enabled).Returns(true);
            processor3.InSequence(sequence).Setup(x => x.Execute());

            Mock<IPostProcessor<FakeAbstractExpander>> processor4 = new(MockBehavior.Strict);
            processor4.Setup(x => x.Enabled).Returns(false);
            processor4.InSequence(sequence).Setup(x => x.Execute());

            fakes.IDependencyFactory.Setup(x => x.ResolveAll<IPostProcessor<FakeAbstractExpander>>()).Returns([processor1.Object, processor2.Object, processor3.Object, processor4.Object]);

            // act
            expander.PostProcess();

            // assert
            processor1.Verify(x => x.Execute(), Times.Once);
            processor2.Verify(x => x.Execute(), Times.Once);
            processor3.Verify(x => x.Execute(), Times.Once);
            processor4.Verify(x => x.Execute(), Times.Never);
        }


        /// <summary>
        /// Test that verifies that the <see cref="AbstractExpander{TExpander}.PreProcess"/> method executes all <see cref="IPreProcessor{TExpander}"/> in the correct order.
        /// </summary>
        [Fact]
        public void VerifyExecutionOfPreProcessorInStrictSequenceOrder()
        {
            // arrange
            MockSequence sequence = new();
            Mock<IPreProcessor<FakeAbstractExpander>> processor1 = new(MockBehavior.Strict);
            processor1.Setup(x => x.Enabled).Returns(true);
            processor1.InSequence(sequence).Setup(x => x.Execute());

            Mock<IPreProcessor<FakeAbstractExpander>> processor2 = new(MockBehavior.Strict);
            processor2.Setup(x => x.Enabled).Returns(true);
            processor2.InSequence(sequence).Setup(x => x.Execute());

            Mock<IPreProcessor<FakeAbstractExpander>> processor3 = new(MockBehavior.Strict);
            processor3.Setup(x => x.Enabled).Returns(true);
            processor3.InSequence(sequence).Setup(x => x.Execute());

            Mock<IPreProcessor<FakeAbstractExpander>> processor4 = new(MockBehavior.Strict);
            processor4.Setup(x => x.Enabled).Returns(false);
            processor4.InSequence(sequence).Setup(x => x.Execute());

            fakes.IDependencyFactory.Setup(x => x.ResolveAll<IPreProcessor<FakeAbstractExpander>>()).Returns([processor1.Object, processor2.Object, processor3.Object, processor4.Object]);

            // act
            expander.PreProcess();

            // assert
            processor1.Verify(x => x.Execute(), Times.Once);
            processor2.Verify(x => x.Execute(), Times.Once);
            processor3.Verify(x => x.Execute(), Times.Once);
            processor4.Verify(x => x.Execute(), Times.Never);
        }
    }
}
