using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Application;
using LiquidVisions.PanthaRhei.Generator.Application.RequestModels;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using Moq;
using Xunit;
using CleanArchitectureResources = LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Resources;

namespace LiquidVisions.PanthaRhei.Generator.CleanArchitecture.Tests.Handlers.Application
{
    public class ExpandBoundariesHandlerInteractorTests
    {
        private readonly CleanArchitectureFakes fakes = new();
        private readonly ExpandBoundariesHandlerInteractor handler;
        private readonly string expectedCreateFolder;

        public ExpandBoundariesHandlerInteractorTests()
        {
            expectedCreateFolder = Path.Combine(fakes.ExpectedCompontentOutputFolder, CleanArchitectureResources.ApplicationBoundariesFolder, fakes.ExpectedEntity.Name.Pluralize());

            fakes.IProjectAgentInteractor.Setup(x => x.GetComponentOutputFolder(fakes.ApplicationComponent.Object)).Returns(fakes.ExpectedCompontentOutputFolder);
            fakes.MockCleanArchitectureExpander(new List<Entity> { fakes.ExpectedEntity });
            handler = new(fakes.CleanArchitectureExpander.Object, fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Constructor_ShouldValidate()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IProjectAgentInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ITemplateInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IDirectory>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<App>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(5));
        }

        [Fact]
        public void Order_ShouldValidate()
        {
            // arrange
            // act
            // assert
            Assert.Equal(3, handler.Order);
        }

        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(nameof(ExpandBoundariesHandlerInteractor), handler.Name);
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
            fakes.GenerationOptions.Setup(x => x.GenerationMode).Returns(mode);

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
            fakes.GenerationOptions.Setup(x => x.GenerationMode).Returns(GenerationModes.Default);
            fakes.GenerationOptions.Setup(x => x.Clean).Returns(clean);

            // act
            // assert
            Assert.Equal(expectedResult, handler.CanExecute);
        }

        [Fact]
        public void Execute_ShouldRenderAnSaveApplicationMapperTemplate()
        {
            // arrange
            string expectedTemplatePath = Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, fakes.CleanArchitectureExpander.Object.Model.Name, fakes.CleanArchitectureExpander.Object.Model.TemplateFolder, $"{CleanArchitectureResources.BoundaryTemplate}.template");
            string[] expectedActions = CleanArchitectureResources.DefaultRequestActions.Split(',', System.StringSplitOptions.TrimEntries);

            // act
            handler.Execute();

            // assert
            fakes.IDirectory.Verify(x => x.Create(expectedCreateFolder), Times.Once);
            fakes.ITemplateInteractor.Verify(x => x.RenderAndSave(expectedTemplatePath, It.IsAny<object>(), It.IsAny<string>()), Times.Exactly(5));
            foreach (string expectedAction in expectedActions)
            {
                fakes.ITemplateInteractor.Verify(
                    x => x.RenderAndSave(
                        expectedTemplatePath,
                        It.Is<object>(x => x.GetHashCode() == new
                        {
                            component = fakes.ApplicationComponent.Object,
                            ActionType = expectedAction,
                            entity = fakes.ExpectedEntity,
                        }.GetHashCode()),
                        Path.Combine(expectedCreateFolder, $"{fakes.ExpectedEntity.ToFileName(expectedAction, "Boundary")}.cs")),
                    Times.Exactly(1));
            }
        }
    }
}
