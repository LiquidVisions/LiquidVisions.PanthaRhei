using System;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases;
using LiquidVisions.PanthaRhei.Generator.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Tests.UseCases
{
    public class CodeGeneratorBuilderTests
    {
        private readonly CodeGeneratorBuilder builder;
        private readonly Fakes fakes = new ();

        public CodeGeneratorBuilderTests()
        {
            builder = new CodeGeneratorBuilder(fakes.IDependencyResolver.Object);
        }

        [Fact]
        public void Build_ParametersHasNoValueAppId_ShouldThrowException()
        {
            // arrange
            Guid id = Guid.NewGuid();
            fakes.Parameters.AppId = id;
            fakes.IAppRepository.Setup(x => x.GetById(id)).Returns((App)null);

            // act
            void Action() => builder.Build();

            // assert
            CodeGenerationException exception = Assert.Throws<CodeGenerationException>(Action);
            Assert.Equal($"No application model available with the provided Id {id}.", exception.Message);
        }

        [Fact]
        public void Build_HappyFlow_ShouldVerify()
        {
            // arrange
            Guid id = Guid.NewGuid();
            fakes.Parameters.AppId = id;
            App app = new();
            fakes.IAppRepository.Setup(x => x.GetById(id)).Returns(app);
            fakes.IDependencyManager.Setup(x => x.Build()).Returns(fakes.IDependencyResolver.Object);

            // act
            builder.Build();

            // assert
            fakes.IDependencyManager.Verify(x => x.AddSingleton(app), Times.Once);
            fakes.IExpanderPluginLoader.Verify(x => x.LoadAllRegisteredPluginsAndBootstrap(app), Times.Once);
            fakes.IDependencyManager.Verify(x => x.Build(), Times.Once);
        }
    }
}
