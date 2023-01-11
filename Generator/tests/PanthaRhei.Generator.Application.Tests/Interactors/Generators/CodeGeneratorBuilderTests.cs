using System;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.Application.Tests.Interactors.Generators
{
    public class CodeGeneratorBuilderTests
    {
        private readonly CodeGeneratorBuilderInteractor interactor;
        private readonly Fakes fakes = new();

        public CodeGeneratorBuilderTests()
        {
            interactor = new CodeGeneratorBuilderInteractor(fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Build_ParametersHasNoValueAppId_ShouldThrowException()
        {
            // arrange
            Guid id = Guid.NewGuid();
            fakes.Parameters.Setup(x => x.AppId).Returns(id);
            fakes.IAppGateway.Setup(x => x.GetById(id)).Returns((App)null);

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
            fakes.Parameters.Setup(x => x.AppId).Returns(id);
            App app = new();
            fakes.IAppGateway.Setup(x => x.GetById(id)).Returns(app);
            fakes.IDependencyManagerInteractor.Setup(x => x.Build()).Returns(fakes.IDependencyFactoryInteractor.Object);

            // act
            interactor.Build();

            // assert
            fakes.IDependencyManagerInteractor.Verify(x => x.AddSingleton(app), Times.Once);
            fakes.IExpanderPluginLoaderInteractor.Verify(x => x.LoadAllRegisteredPluginsAndBootstrap(app), Times.Once);
            fakes.IDependencyManagerInteractor.Verify(x => x.Build(), Times.Once);
        }
    }
}
