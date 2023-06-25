using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using Moq;
using Xunit;
using CleanArchitectureResources = LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Resources;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests.Handlers.Api
{
    public class ExpandViewModelMapperHandlerInteractorTests
    {
        private readonly CleanArchitectureFakes fakes = new();
        private readonly ExpandViewModelMapperHandlerInteractor handler;

        public ExpandViewModelMapperHandlerInteractorTests()
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
            fakes.IDependencyFactory.Verify(x => x.Get<ITemplate>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<IDirectory>(), Times.Once);
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
            Assert.Equal(15, handler.Order);
        }

        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(nameof(ExpandViewModelMapperHandlerInteractor), handler.Name);
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
        public void Execute_ShouldRenderAnSaveViewModelTemplate()
        {
            // arrange
            string expectedViewModelFolder = Path.Combine(fakes.ExpectedCompontentOutputFolder, CleanArchitectureResources.ViewModelMapperFolder);
            string expectedTemplatePath = Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, fakes.CleanArchitectureExpander.Object.Model.Name, fakes.CleanArchitectureExpander.Object.Model.TemplateFolder, $"{CleanArchitectureResources.ViewModelMapperTemplate}.template");
            string expectedFilePath = Path.Combine(expectedViewModelFolder, $"{fakes.ExpectedEntity.Name}ViewModelMapper.cs");

            // act
            handler.Execute();

            // assert
            fakes.IDirectory.Verify(x => x.Create(expectedViewModelFolder), Times.Once);
            fakes.ITemplate.Verify(
                x => x.RenderAndSave(
                    expectedTemplatePath,
                    It.Is<object>(x => x.GetHashCode() == new
                    {
                        Entity = fakes.ExpectedEntity,
                        component = fakes.ApiComponent.Object,
                        applicationComponent = fakes.ApplicationComponent.Object,
                    }
                    .GetHashCode()), expectedFilePath), Times.Once);
        }
    }
}
