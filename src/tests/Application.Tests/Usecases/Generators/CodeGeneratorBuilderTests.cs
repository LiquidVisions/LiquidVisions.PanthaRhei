using System;
using LiquidVisions.PanthaRhei.Application.Usecases.Generators;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Usecases.Generators
{
    /// <summary>
    /// Tests for <see cref="CodeGeneratorBuilder"/>.
    /// </summary>
    public class CodeGeneratorBuilderTests
    {
        private readonly CodeGeneratorBuilder interactor;
        private readonly Mock<IGetRepository<App>> mockedGetGateway = new();
        private readonly ApplicationFakes fakes = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGeneratorBuilderTests"/> class.
        /// </summary>
        public CodeGeneratorBuilderTests()
        {
            fakes.IDependencyFactory.Setup(x => x.Resolve<IGetRepository<App>>()).Returns(mockedGetGateway.Object);

            interactor = new CodeGeneratorBuilder(fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Tests the <see cref="CodeGeneratorBuilder.Build"/> method while throwing an <seealso cref="CodeGenerationException"/>.
        /// </summary>
        [Fact]
        public void BuildParametersHasNoValueAppIdShouldThrowException()
        {
            // arrange
            Guid id = Guid.NewGuid();
            fakes.GenerationOptions.Setup(x => x.AppId).Returns(id);
            mockedGetGateway.Setup(x => x.GetById(id)).Returns((App)null);

            // act
            void Action() => interactor.Build();

            // assert
            CodeGenerationException exception = Assert.Throws<CodeGenerationException>(Action);
            Assert.Equal($"No application model available with the provided Id {id}.", exception.Message);
        }

        /// <summary>
        /// Tests the <see cref="CodeGeneratorBuilder.Build"/> method.
        /// </summary>
        [Fact]
        public void BuildHappyFlowShouldVerify()
        {
            // arrange
            Guid id = Guid.NewGuid();
            fakes.GenerationOptions.Setup(x => x.AppId).Returns(id);
            App app = new();
            mockedGetGateway.Setup(x => x.GetById(id)).Returns(app);
            fakes.IDependencyManager.Setup(x => x.Build()).Returns(fakes.IDependencyFactory.Object);

            // act
            interactor.Build();

            // assert
            fakes.IDependencyManager.Verify(x => x.AddSingleton(app), Times.Once);
            fakes.IExpanderPluginLoader.Verify(x => x.LoadAllRegisteredPluginsAndBootstrap(app), Times.Once);
            fakes.IDependencyManager.Verify(x => x.Build(), Times.Once);
        }
    }
}
