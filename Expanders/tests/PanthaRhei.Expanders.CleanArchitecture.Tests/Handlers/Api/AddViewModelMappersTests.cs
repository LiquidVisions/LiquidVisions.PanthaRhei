﻿using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.CleanArchitecture.Tests.Handlers.Api
{
    public class AddViewModelMappersTests
    {
        private readonly CleanArchitectureFakes fakes = new();
        private readonly AddViewModelMappers handler;
        private readonly Entity expectedEntity = new();

        public AddViewModelMappersTests()
        {
            expectedEntity.Name = "JustATestEntity";
            fakes.IProjectAgentInteractor.Setup(x => x.GetComponentOutputFolder(fakes.ApiComponent.Object)).Returns(fakes.ExpectedCompontentOutputFolder);
            fakes.MockCleanArchitectureExpander(new List<Entity> { expectedEntity });
            handler = new(fakes.CleanArchitectureExpanderInteractor.Object, fakes.IDependencyFactoryInteractor.Object);
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
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<Domain.Parameters>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<App>(), Times.Once);

            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(5));
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
            Assert.Equal(nameof(AddViewModelMappers), handler.Name);
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
        public void Execute_ShouldRenderAnSaveViewModelTemplate()
        {
            // arrange
            string expectedViewModelFolder = Path.Combine(fakes.ExpectedCompontentOutputFolder, Resources.ViewModelMapperFolder);
            string expectedTemplatePath = Path.Combine(fakes.Parameters.Object.ExpandersFolder, fakes.CleanArchitectureExpanderInteractor.Object.Model.Name, fakes.CleanArchitectureExpanderInteractor.Object.Model.TemplateFolder, $"{Resources.ViewModelMapperTemplate}.template");
            string expectedFilePath = Path.Combine(expectedViewModelFolder, $"{expectedEntity.Name}ModelMapper.cs");
            // act
            handler.Execute();

            // assert
            fakes.IDirectory.Verify(x => x.Create(expectedViewModelFolder), Times.Once);
            fakes.ITemplateInteractor.Verify(
                x => x.RenderAndSave(
                    expectedTemplatePath,
                    It.Is<object>(x => x.GetHashCode() == new
                        {
                            Entity = expectedEntity,
                            component = fakes.ApiComponent.Object,
                            clientComponent = fakes.ClientComponent.Object,
                            applicationComponent = fakes.ApplicationComponent.Object,
                        }
                    .GetHashCode()), expectedFilePath), Times.Once);
        }
    }
}
