using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Infrastructure;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
using Moq;
using Xunit;
using CleanArchitectureResources = LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Resources;

namespace LiquidVisions.PanthaRhei.Generator.CleanArchitecture.Tests.Handlers.Infrastructure
{
    public class ConfigureInfrastructureLibraryTests
    {
        private readonly ConfigureInfrastructureLibrary handler;
        private readonly CleanArchitectureFakes fakes = new();
        private readonly string fullPathToBootstrapperFile;

        public ConfigureInfrastructureLibraryTests()
        {
            fakes.IProjectAgentInteractor.Setup(x => x.GetComponentOutputFolder(fakes.InfrastructureComponent.Object)).Returns(fakes.ExpectedCompontentOutputFolder);
            fakes.MockCleanArchitectureExpander(new List<Entity> { fakes.ExpectedEntity });
            handler = new(fakes.CleanArchitectureExpander.Object, fakes.IDependencyFactoryInteractor.Object);

            fullPathToBootstrapperFile = Path.Combine(fakes.ExpectedCompontentOutputFolder, CleanArchitectureResources.DependencyInjectionBootstrapperFile);
        }

        [Fact]
        public void Constructor_ShouldValidate()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IWriterInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IProjectAgentInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ITemplateInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<Parameters>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<App>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(5));

            fakes.IWriterInteractor.Verify(x => x.Load(fullPathToBootstrapperFile), Times.Once);
        }

        [Fact]
        public void Order_ShouldValidate()
        {
            // arrange
            // act
            // assert
            Assert.Equal(10, handler.Order);
        }

        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(nameof(ConfigureInfrastructureLibrary), handler.Name);
        }

        [Theory]
        [InlineData(GenerationModes.Default, true)]
        [InlineData(GenerationModes.Migrate, false)]
        [InlineData(GenerationModes.Extend, true)]
        [InlineData(GenerationModes.Deploy, false)]
        [InlineData(GenerationModes.None, false)]
        public void CanExecute_ShouldBeFalse(GenerationModes mode, bool expectedResult)
        {
            // arrange
            fakes.Parameters.Setup(x => x.GenerationMode).Returns(mode);

            // act
            // assert
            Assert.Equal(expectedResult, handler.CanExecute);
        }

        [Theory]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public void CanExecute_ShouldOnlyBeTrueWhenCleanParameterIsSetToTrue(bool clean, bool expectedResult)
        {
            // arrange
            fakes.Parameters.Setup(x => x.GenerationMode).Returns(GenerationModes.Default);
            fakes.Parameters.Setup(x => x.Clean).Returns(clean);

            // act
            // assert
            Assert.Equal(expectedResult, handler.CanExecute);
        }

        [Fact]
        public void Execute_ShouldCreateAndSaveTemplate()
        {
            // arrange
            string expectedTemplateBaseBath = Path.Combine(fakes.Parameters.Object.ExpandersFolder, fakes.CleanArchitectureExpander.Object.Model.Name, fakes.CleanArchitectureExpander.Object.Model.TemplateFolder, $"{CleanArchitectureResources.InfrastructureDependencyInjectionBootstrapperTemplate}.template");
            string expectedRenderResult = "ExpectedRenderResult";
            fakes.ITemplateInteractor.Setup(x => x.Render(expectedTemplateBaseBath, It.Is<object>(x => x.GetHashCode() == new { Entity = fakes.ExpectedEntity }.GetHashCode()))).Returns(expectedRenderResult);

            // act
            handler.Execute();

            // assert
            fakes.ITemplateInteractor.Verify(x => x.Render(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
            fakes.ITemplateInteractor.Verify(
                x => x.Render(
                    expectedTemplateBaseBath,
                    It.Is<object>(x => x.GetHashCode() == new { Entity = fakes.ExpectedEntity }.GetHashCode())),
                Times.Once);
            fakes.IWriterInteractor.Verify(x => x.AddOrReplaceMethod(expectedRenderResult), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.AppendToMethod("AddInfrastructureLayer", $"            services.Add{fakes.ExpectedEntity.Name}();"), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.Save(fullPathToBootstrapperFile), Times.Once);
        }
    }
}
