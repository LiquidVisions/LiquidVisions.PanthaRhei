﻿using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Application;
using Moq;
using System.Collections.Generic;
using System.IO;
using Xunit;
using CleanArchitectureResources = LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Resources;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests.Handlers.Application
{
    public class ExpandRequestModelsHandlerInteractorTests
    {
        private readonly ExpandRequestModelsTask handler;
        private readonly CleanArchitectureFakes fakes = new ();
        private readonly string expectedCreateFolder;

        public ExpandRequestModelsHandlerInteractorTests()
        {
            expectedCreateFolder = Path.Combine(fakes.ExpectedCompontentOutputFolder, CleanArchitectureResources.RequestModelsFolder, fakes.ExpectedEntity.Name.Pluralize());

            fakes.MockCleanArchitectureExpander(new List<Entity> { fakes.ExpectedEntity });
            handler = new(fakes.CleanArchitectureExpander.Object, fakes.IDependencyFactory.Object);
        }

        [Fact]
        public void Constructor_ShouldValidate()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Resolve<ITemplate>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IDirectory>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<App>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(4));
        }

        [Fact]
        public void Order_ShouldValidate()
        {
            // arrange
            // act
            // assert
            Assert.Equal(18, handler.Order);
        }

        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(nameof(ExpandRequestModelsTask), handler.Name);
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
        public void Execute_ShouldExecuteAndSaveTemplate()
        {
            // arrange
            App expectedApp = fakes.SetupApp();
            string expectedTemplateBaseBath = Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, fakes.CleanArchitectureExpander.Object.Model.Name, PanthaRhei.Domain.Resources.TemplatesFolder);
            string[] actions = CleanArchitectureResources.DefaultRequestActions.Split(',', System.StringSplitOptions.TrimEntries);

            // act
            handler.Execute();

            // assert
            fakes.IDirectory.Verify(x => x.Create(expectedCreateFolder), Times.Once);
            foreach (string action in actions)
            {
                fakes.ITemplate.Verify(x => x.RenderAndSave(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()), Times.Exactly(5));
                fakes.ITemplate.Verify(
                    x => x.RenderAndSave(
                        Path.Combine(expectedTemplateBaseBath, $"{CleanArchitectureResources.RequestModelTemplate}.template"),
                        It.Is<object>(x => x.GetHashCode() == new
                        {
                            Action = action,
                            NS = fakes.CleanArchitectureExpander.Object.Model.Name,
                            NameSpace = $"{fakes.ApplicationComponent.Object.GetComponentNamespace(expectedApp, CleanArchitectureResources.RequestModelsFolder)}.{fakes.ExpectedEntity.Name.Pluralize()}",
                            Entity = fakes.ExpectedEntity,
                        }.GetHashCode()),
                        Path.Combine(expectedCreateFolder, $"{ExpandRequestModelsTask.ToFileName(action, fakes.ExpectedEntity)}.cs")),
                    Times.Once);
            }
        }
    }
}
