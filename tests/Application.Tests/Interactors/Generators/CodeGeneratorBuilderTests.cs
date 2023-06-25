using System;
using LiquidVisions.PanthaRhei.Application.Usecases.Generators;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Interactors.Generators
{
    public class CodeGeneratorBuilderTests
    {
        private readonly CodeGeneratorBuilder interactor;
        private readonly Mock<IGetRepository<App>> mockedGetGateway = new();
        private readonly Fakes fakes = new();

        public CodeGeneratorBuilderTests()
        {
            fakes.IDependencyFactory.Setup(x => x.Get<IGetRepository<App>>()).Returns(mockedGetGateway.Object);

            interactor = new CodeGeneratorBuilder(fakes.IDependencyFactory.Object);
        }

        [Fact]
        public void Build_ParametersHasNoValueAppId_ShouldThrowException()
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

        [Fact]
        public void Build_HappyFlow_ShouldVerify()
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
