﻿using System.Collections.Generic;
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
    public class ExpandApplicationMappersHandlerInteractorTests
    {
        private readonly CleanArchitectureFakes fakes = new ();
        private readonly ExpandApplicationMappersTask handler;

        public ExpandApplicationMappersHandlerInteractorTests()
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
            Assert.Equal(4, handler.Order);
        }

        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(nameof(ExpandApplicationMappersTask), handler.Name);
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
            string expectedTemplatePath = Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, fakes.CleanArchitectureExpander.Object.Model.Name, PanthaRhei.Domain.Resources.TemplatesFolder, $"{CleanArchitectureResources.ApplicationMapperTemplate}.template");
            string expectedCreateFolder = Path.Combine(fakes.ExpectedCompontentOutputFolder, CleanArchitectureResources.ApplicationMapperFolder, fakes.ExpectedEntity.Name.Pluralize());
            string[] expectedActions = new string[] { "Create", "Update" };

            // act
            handler.Execute();

            // assert
            fakes.IDirectory.Verify(x => x.Create(expectedCreateFolder), Times.Once);
            fakes.ITemplate.Verify(x => x.RenderAndSave(expectedTemplatePath, It.IsAny<object>(), It.IsAny<string>()), Times.Exactly(2));
            foreach (string expectedAction in expectedActions)
            {
                fakes.ITemplate.Verify(
                x => x.RenderAndSave(
                    expectedTemplatePath,
                    It.Is<object>(x => x.GetHashCode() == new
                    {
                        component = fakes.ApplicationComponent.Object,
                        Action = expectedAction,
                        Entity = fakes.ExpectedEntity,
                    }.GetHashCode()),
                    Path.Combine(expectedCreateFolder, $"{expectedAction}{fakes.ExpectedEntity.Name}RequestModelMapper.cs")),
                Times.Exactly(1));
            }
        }
    }
}
