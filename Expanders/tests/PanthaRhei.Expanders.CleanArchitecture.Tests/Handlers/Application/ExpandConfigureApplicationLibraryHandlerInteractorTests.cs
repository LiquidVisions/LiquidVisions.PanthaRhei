using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Application;
using Moq;
using Xunit;
using CleanArchitectureResources = LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Resources;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests.Handlers.Application
{
    public class ExpandConfigureApplicationLibraryHandlerInteractorTests
    {
        private readonly ExpandConfigureApplicationLibraryTask handler;
        private readonly CleanArchitectureFakes fakes = new();
        private readonly string expectedRenderResult = "JustAFakeRenderedResult";

        public ExpandConfigureApplicationLibraryHandlerInteractorTests()
        {
            fakes.MockCleanArchitectureExpander(new List<Entity> { fakes.ExpectedEntity });
            handler = new(fakes.CleanArchitectureExpander.Object, fakes.IDependencyFactory.Object);
        }

        [Fact]
        public void Constructor_ShouldValidate()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Get<IWriter>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<ITemplate>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<App>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(4));
        }

        [Fact]
        public void Order_ShouldValidate()
        {
            // arrange
            // act
            // assert
            Assert.Equal(7, handler.Order);
        }

        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(nameof(ExpandConfigureApplicationLibraryTask), handler.Name);
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
            fakes.GenerationOptions.Setup(x => x.Modes).Returns(mode);

            // act
            // assert
            Assert.Equal(expectedResult, handler.Enabled);
        }

        [Theory]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public void CanExecute_ShouldOnlyBeTrueWhenCleanParameterIsSetToTrue(bool clean, bool expectedResult)
        {
            // arrange
            fakes.GenerationOptions.Setup(x => x.Modes).Returns(GenerationModes.Default);
            fakes.GenerationOptions.Setup(x => x.Clean).Returns(clean);

            // act
            // assert
            Assert.Equal(expectedResult, handler.Enabled);
        }

        [Fact]
        public void Execute_ShouldModifyBootstrapperFile()
        {
            // arrange
            App app = fakes.SetupApp();
            string expectedNameSpace = fakes.ApplicationComponent.Object.GetComponentNamespace(app);
            string expectedFullPathToBootstrapperFile = Path.Combine(fakes.ExpectedCompontentOutputFolder, CleanArchitectureResources.DependencyInjectionBootstrapperFile);
            string expectedFullPathToTemplate = Path.Combine(
                fakes.GenerationOptions.Object.ExpandersFolder,
                fakes.CleanArchitectureExpander.Object.Model.Name,
                fakes.CleanArchitectureExpander.Object.Model.TemplateFolder,
                $"{CleanArchitectureResources.ApplicationDependencyInjectionBootstrapperTemplate}.template");

            fakes.ITemplate.Setup(
                x => x.Render(
                    expectedFullPathToTemplate,
                    It.Is<object>(x => x.GetHashCode() == new { Entity = fakes.ExpectedEntity }.GetHashCode())))
                .Returns(expectedRenderResult);

            // act
            handler.Execute();

            // assert
            fakes.IWriter.Verify(x => x.Load(expectedFullPathToBootstrapperFile), Times.Once);

            fakes.ITemplate.Verify(x => x.Render(expectedFullPathToTemplate, It.Is<object>(x => x.GetHashCode() == new { Entity = fakes.ExpectedEntity }.GetHashCode())), Times.Once);
            fakes.IWriter.Verify(x => x.AddOrReplaceMethod(expectedRenderResult), Times.Once);
            fakes.IWriter.Verify(x => x.AddNameSpace($"{expectedNameSpace}.Boundaries.{fakes.ExpectedEntity.Name.Pluralize()}"), Times.Once);
            fakes.IWriter.Verify(x => x.AddNameSpace($"{expectedNameSpace}.Boundaries.{fakes.ExpectedEntity.Name.Pluralize()}"), Times.Once);
            fakes.IWriter.Verify(x => x.AddNameSpace($"{expectedNameSpace}.Mappers.{fakes.ExpectedEntity.Name.Pluralize()}"), Times.Once);
            fakes.IWriter.Verify(x => x.AddNameSpace($"{expectedNameSpace}.RequestModels.{fakes.ExpectedEntity.Name.Pluralize()}"), Times.Once);
            fakes.IWriter.Verify(x => x.AddNameSpace($"{expectedNameSpace}.Validators.{fakes.ExpectedEntity.Name.Pluralize()}"), Times.Once);
            fakes.IWriter.Verify(x => x.AddNameSpace($"{expectedNameSpace}.Gateways"), Times.Once);
            fakes.IWriter.Verify(x => x.AddNameSpace(It.IsAny<string>()), Times.Exactly(6));

            fakes.IWriter.Verify(x => x.Save(expectedFullPathToBootstrapperFile), Times.Once);
        }
    }
}
