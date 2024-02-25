using System;
using System.Reflection;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Tests.Mocks;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Initializers;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.PostProcessors;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Preprocessors;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Rejuvenators;
using LiquidVisions.PanthaRhei.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Domain.Tests.UseCases.Generators
{
    /// <summary>
    /// Tests the <see cref="AbstractExpanderDependencyManager{TExpander}"/> class.
    /// </summary>
    public class AbstractExpanderDependencyManagerTests
    {
        private readonly Fakes fakes = new();
        private readonly FakeExpanderDependencyManager manager;
        private readonly Mock<IAssemblyManager> mockedAssemblyManager = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractExpanderDependencyManagerTests"/> class.
        /// </summary>
        public AbstractExpanderDependencyManagerTests()
        {
            Expander expander = new()
            {
                Name = "FakeExpander"
            };
            fakes.IDependencyManager.Setup(x => x.Build()).Returns(fakes.IDependencyFactory.Object);
            fakes.IDependencyFactory.Setup(x => x.Resolve<IAssemblyManager>()).Returns(mockedAssemblyManager.Object);


            manager = new FakeExpanderDependencyManager(expander, fakes.IDependencyManager.Object);
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
            fakes.IDependencyFactory.Verify(x => x.Resolve<ILogger>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IAssemblyManager>(), Times.Once);

            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(2));
        }

        /// <summary>
        /// Tests that the Register method adds all processing items.
        /// </summary>
        [Fact]
        public void RegisterShouldAddTransientAllProcessingItems()
        {
            // arrange
            Mock<Type> preprocessors = MockPreProcessors();
            Mock<Type> expanders = MockExpanders();
            Mock<Type> tasks = MockedExpanderTaskTypes();
            Mock<Type> rejuvenators = MockRejuvenators();
            Mock<Type> harvesters = MockHarvesters();
            Mock<Type> postprocessors = MockPostProcessors();

            Mock<Assembly> mockedAssembly = new();
            mockedAssembly.Setup(x => x.GetExportedTypes()).Returns([preprocessors.Object, expanders.Object, tasks.Object, rejuvenators.Object, harvesters.Object, postprocessors.Object]);
            mockedAssemblyManager.Setup(x => x.GetAssembly(It.IsAny<Type>())).Returns(mockedAssembly.Object);

            // act
            manager.Register();

            // assert
            mockedAssemblyManager.Verify(x => x.GetAssembly(It.IsAny<Type>()), Times.Once);
            fakes.IDependencyManager.Verify(x => x.AddTransient(typeof(IPreProcessor<FakeExpander>), typeof(InstallDotNetTemplate<FakeExpander>)), Times.Once);
            fakes.IDependencyManager.Verify(x => x.AddTransient(typeof(IPreProcessor<FakeExpander>), preprocessors.Object), Times.Once);

            fakes.IDependencyManager.Verify(x => x.AddTransient(typeof(IPostProcessor<FakeExpander>), typeof(UnInstallDotNetTemplate<FakeExpander>)), Times.Once);
            fakes.IDependencyManager.Verify(x => x.AddTransient(typeof(IPostProcessor<FakeExpander>), postprocessors.Object), Times.Once);

            fakes.IDependencyManager.Verify(x => x.AddTransient(typeof(IExpander), expanders.Object), Times.Once);
            fakes.IDependencyManager.Verify(x => x.AddTransient(expanders.Object, expanders.Object), Times.Once);
            fakes.IDependencyManager.Verify(x => x.AddTransient(typeof(IExpanderTask<FakeExpander>), tasks.Object), Times.Once);

            fakes.IDependencyManager.Verify(x => x.AddTransient(typeof(IRejuvenator<FakeExpander>), typeof(RegionRejuvenator<FakeExpander>)), Times.Once);
            fakes.IDependencyManager.Verify(x => x.AddTransient(typeof(IRejuvenator<FakeExpander>), rejuvenators.Object), Times.Once);

            fakes.IDependencyManager.Verify(x => x.AddTransient(typeof(IHarvester<FakeExpander>), typeof(RegionHarvester<FakeExpander>)), Times.Once);
            fakes.IDependencyManager.Verify(x => x.AddTransient(typeof(IHarvester<FakeExpander>), harvesters.Object), Times.Once);
            fakes.IDependencyManager.Verify(x => x.AddTransient(It.IsAny<Type>(), It.IsAny<Type>()), Times.Exactly(11));
        }

        /// <summary>
        /// Tests that abstract expander tasks should not be added transient.
        /// </summary>
        [Fact]
        public void RegisterAbstractExpanderTasksShouldNotBeAddedTransient()
        {
            // arrange
      
            Mock<Type> result = new();
            result.Setup(x => x.GetInterfaces()).Returns([typeof(IExpanderTask<FakeExpander>), typeof(FakeExpanderTask)]);

            Mock<Assembly> mockedAssembly = new();
            mockedAssembly.Setup(x => x.GetExportedTypes()).Returns([MockExpanders().Object, result.Object]);
            mockedAssemblyManager.Setup(x => x.GetAssembly(It.IsAny<Type>())).Returns(mockedAssembly.Object);
            
            // act
            manager.Register();

            // assert
            fakes.IDependencyManager.Verify(x => x.AddTransient(typeof(IExpanderTask<FakeExpander>), result.Object), Times.Once);
        }

        /// <summary>
        /// Tests if an <see cref="InitializationException"/> is thrown when no expanders are found.
        /// </summary>
        [Fact]
        public void AssembliesWithoutExpandersShouldThrowInitializationException()
        {
            // arrange
            Mock<Assembly> mockedAssembly = new();
            mockedAssemblyManager.Setup(x => x.GetAssembly(It.IsAny<Type>())).Returns(mockedAssembly.Object);

            // act
            // assert
            InitializationException exception = Assert.Throws<InitializationException>(() => manager.Register());
            Assert.Equal($"Unable to load plugin '{nameof(FakeExpander)}'. No valid {nameof(IExpander)} derivatives found. The derivatives should be a non-abstract class.", exception.Message);
        }

        private Mock<Type> MockedExpanderTaskTypes()
        {
            Mock<Type> result = new();
            result.Setup(x => x.GetInterfaces()).Returns([typeof(IExpanderTask<FakeExpander>)]);

            return result;
        }

        private static Mock<Type> MockExpanders()
        {
            Mock<Type> result = new();
            result.Setup(x => x.GetInterfaces()).Returns([typeof(IExpander)]);

            return result;
        }

        private static Mock<Type> MockPreProcessors()
        {
            Mock<Type> result = new();
            result.Setup(x => x.GetInterfaces()).Returns([typeof(IPreProcessor<FakeExpander>)]);

            return result;
        }

        private static Mock<Type> MockPostProcessors()
        {
            Mock<Type> result = new();
            result.Setup(x => x.GetInterfaces()).Returns([typeof(IPostProcessor<FakeExpander>)]);

            return result;
        }

        private static Mock<Type> MockRejuvenators()
        {
            Mock<Type> result = new();
            result.Setup(x => x.GetInterfaces()).Returns([typeof(IRejuvenator<FakeExpander>)]);

            return result;
        }

        private static Mock<Type> MockHarvesters()
        {
            Mock<Type> result = new();
            result.Setup(x => x.GetInterfaces()).Returns([typeof(IHarvester<FakeExpander>)]);

            return result;
        }
    }
}
