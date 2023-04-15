﻿using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Application;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
using Moq;
using Xunit;
using CleanArchitectureResources = LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Resources;

namespace LiquidVisions.PanthaRhei.Generator.CleanArchitecture.Tests.Handlers.Application
{
    public class ExpandConfigureApplicationLibraryHandlerInteractorTests
    {
        private readonly ExpandConfigureApplicationLibraryHandlerInteractor handler;
        private readonly CleanArchitectureFakes fakes = new();
        private readonly string expectedRenderResult = "JustAFakeRenderedResult";

        public ExpandConfigureApplicationLibraryHandlerInteractorTests()
        {
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
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IWriterInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IProjectAgentInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ITemplateInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ExpandRequestModel>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<App>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(5));
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
            Assert.Equal(nameof(ExpandConfigureApplicationLibraryHandlerInteractor), handler.Name);
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
        public void Execute_ShouldModifyBootstrapperFile()
        {
            // arrange
            App app = fakes.SetupApp();
            string expectedNameSpace = fakes.ApplicationComponent.Object.GetComponentNamespace(app);
            string expectedFullPathToBootstrapperFile = Path.Combine(fakes.ExpectedCompontentOutputFolder, CleanArchitectureResources.DependencyInjectionBootstrapperFile);
            string expectedFullPathToTemplate = Path.Combine(
                fakes.Parameters.Object.ExpandersFolder,
                fakes.CleanArchitectureExpander.Object.Model.Name,
                fakes.CleanArchitectureExpander.Object.Model.TemplateFolder,
                $"{CleanArchitectureResources.ApplicationDependencyInjectionBootstrapperTemplate}.template");

            fakes.ITemplateInteractor.Setup(
                x => x.Render(
                    expectedFullPathToTemplate,
                    It.Is<object>(x => x.GetHashCode() == new { Entity = fakes.ExpectedEntity }.GetHashCode())))
                .Returns(expectedRenderResult);

            // act
            handler.Execute();

            // assert
            fakes.IWriterInteractor.Verify(x => x.Load(expectedFullPathToBootstrapperFile), Times.Once);

            fakes.ITemplateInteractor.Verify(x => x.Render(expectedFullPathToTemplate, It.Is<object>(x => x.GetHashCode() == new { Entity = fakes.ExpectedEntity }.GetHashCode())), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.AddOrReplaceMethod(expectedRenderResult), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.AddNameSpace($"{expectedNameSpace}.Boundaries.{fakes.ExpectedEntity.Name.Pluralize()}"), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.AddNameSpace($"{expectedNameSpace}.Boundaries.{fakes.ExpectedEntity.Name.Pluralize()}"), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.AddNameSpace($"{expectedNameSpace}.Mappers.{fakes.ExpectedEntity.Name.Pluralize()}"), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.AddNameSpace($"{expectedNameSpace}.RequestModels.{fakes.ExpectedEntity.Name.Pluralize()}"), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.AddNameSpace($"{expectedNameSpace}.Validators.{fakes.ExpectedEntity.Name.Pluralize()}"), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.AddNameSpace($"{expectedNameSpace}.Gateways"), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.AddNameSpace(It.IsAny<string>()), Times.Exactly(6));

            fakes.IWriterInteractor.Verify(x => x.Save(expectedFullPathToBootstrapperFile), Times.Once);
        }
    }
}
