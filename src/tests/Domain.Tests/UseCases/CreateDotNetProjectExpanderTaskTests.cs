using System;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Tests.Mocks;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;
using LiquidVisions.PanthaRhei.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Domain.Tests.UseCases
{
    /// <summary>
    /// Tests for <see cref="CreateDotNetProjectExpanderTask{TExpander}"/>.
    /// </summary>
    public class CreateDotNetProjectExpanderTaskTests
    {
        private readonly Fakes fakes = new();
        private readonly CreateDotNetProjectExpanderTask<FakeExpander> task;
        private readonly Component component = new();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CreateDotNetProjectExpanderTaskTests()
        {
            Expander expander = new();
            expander.Components.Add(component);

            FakeExpander fakeExpander = new();
            fakeExpander.SetModel(expander);

            task = new(fakeExpander, fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Injecting null reference on expander should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [Fact]
        public void InjectingNullReferenceOnExpanderShouldThrowArgumentNullException()
        {
            // arrange

            // act

            // assert
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
            new CreateDotNetProjectExpanderTask<FakeExpander>(null, fakes.IDependencyFactory.Object));

            Assert.Equal("Value cannot be null. (Parameter 'expander')", exception.Message);
        }

        /// <summary>
        /// Injecting null reference on expander should throw <see cref="ArgumentNullException"/>.
        /// </summary>
        [Fact]
        public void InjectingNullReferenceOnIDependencyFactoryShouldThrowArgumentNullException()
        {
            // arrange
            // act
            // assert
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
            new CreateDotNetProjectExpanderTask<FakeExpander>(new FakeExpander(), null));

            Assert.Equal("Value cannot be null. (Parameter 'dependencyFactory')", exception.Message);
        }

        /// <summary>
        /// Order should be zero.
        /// </summary>
        [Fact]
        public void OrderShouldBeZero()
        {
            // arrange
            // act
            // assert
            Assert.Equal(0, task.Order);
        }

        /// <summary>
        /// Verifies the initialization.
        /// </summary>
        [Fact]
        public void InitializationShouldVerify()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IApplication>(), Times.Once);
            fakes.IDependencyFactory.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Task should only be enabled when option clean is set to true.
        /// </summary>
        /// <param name="clean"></param>
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TaskShouldOnlyBeEnabledWhenOptionCleanIsSetToTrue(bool clean)
        {
            // arrange
            fakes.GenerationOptions.Setup(x => x.Clean).Returns(clean);

            // act
            // assert
            Assert.Equal(clean, task.Enabled);
        }


        /// <summary>
        /// Task should execute <see cref="IApplication.MaterializeComponent(Component)"/> with component."/>
        /// </summary>
        [Fact]
        public void TaskShouldExecuteWithComponent()
        {
            // arrange

            // act
            task.Execute();

            // assert
            fakes.IApplication.Verify(x => x.MaterializeComponent(component), Times.Once);
            fakes.IApplication.VerifyNoOtherCalls();

        }
    }
}
