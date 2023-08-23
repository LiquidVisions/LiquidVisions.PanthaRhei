using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Application;
using Moq;
using Xunit;
using CleanArchitectureResources = LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Resources;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests.Handlers.Application
{
    public class ExpandInteractorsHandlerInteractorTests
    {
        private readonly CleanArchitectureFakes fakes = new ();
        private readonly ExpandInteractorsTask handler;
        private readonly string expectedCreateFolder;

        public ExpandInteractorsHandlerInteractorTests()
        {
            expectedCreateFolder = Path.Combine(fakes.ExpectedCompontentOutputFolder, CleanArchitectureResources.InteractorFolder, fakes.ExpectedEntity.Name.Pluralize());

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
            Assert.Equal(5, handler.Order);
        }

        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(nameof(ExpandInteractorsTask), handler.Name);
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
        public void Execute_ShouldRenderAnSaveApplicationMapperTemplate()
        {
            // arrange
            string[] expectedActions = CleanArchitectureResources.DefaultRequestActions.Split(',', System.StringSplitOptions.TrimEntries);

            // act
            handler.Execute();

            // assert
            fakes.IDirectory.Verify(x => x.Create(expectedCreateFolder), Times.Once);
            foreach (string expectedAction in expectedActions)
            {
                string expectedTemplatePath = Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, fakes.CleanArchitectureExpander.Object.Model.Name, PanthaRhei.Domain.Resources.TemplatesFolder, $"{expectedAction}{CleanArchitectureResources.InteractorTemplate}.template");
                fakes.ITemplate.Verify(x => x.RenderAndSave(expectedTemplatePath, It.IsAny<object>(), It.IsAny<string>()), Times.Exactly(1));

                fakes.ITemplate.Verify(
                    x => x.RenderAndSave(
                        expectedTemplatePath,
                        It.Is<object>(x => x.GetHashCode() == new
                        {
                            component = fakes.ApplicationComponent.Object,
                            Entity = fakes.ExpectedEntity,
                        }.GetHashCode()),
                        Path.Combine(expectedCreateFolder, $"{fakes.ExpectedEntity.ToFileName(expectedAction, "Interactor")}.cs")),
                    Times.Exactly(1));
            }
        }
    }
}
