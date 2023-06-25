using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Infrastructure;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Interactors.Templates;
using Moq;
using Xunit;
using CleanArchitectureResources = LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Resources;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests.Handlers.Infrastructure
{
    public class ExpandRepositoryHandlerInteractorTests
    {
        private readonly ExpandRepositoryHandlerInteractor handler;
        private readonly string outputFolder;
        private readonly CleanArchitectureFakes fakes = new();
        private readonly List<Entity> allEntities;

        public ExpandRepositoryHandlerInteractorTests()
        {
            allEntities = fakes.GetValidEntities();
            fakes.MockCleanArchitectureExpander(allEntities);
            handler = new(fakes.CleanArchitectureExpander.Object, fakes.IDependencyFactoryInteractor.Object);

            outputFolder = Path.Combine(fakes.CleanArchitectureExpander.Object.GetComponentOutputFolder(fakes.InfrastructureComponent.Object), CleanArchitectureResources.RepositoryFolder);
        }

        [Fact]
        public void Constructor_ShouldValidate()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ITemplateInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<App>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(4));
        }

        [Fact]
        public void Order_ShouldValidate()
        {
            // arrange
            // act
            // assert
            Assert.Equal(11, handler.Order);
        }

        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(nameof(ExpandRepositoryHandlerInteractor), handler.Name);
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
            Assert.Equal(expectedResult, handler.CanExecute);
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
            Assert.Equal(expectedResult, handler.CanExecute);
        }

        [Fact]
        public void Execute_ShouldCreateAndSaveTemplate()
        {
            // arrange
            string expectedTemplateBaseBath = Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, fakes.CleanArchitectureExpander.Object.Model.Name, fakes.CleanArchitectureExpander.Object.Model.TemplateFolder, $"{CleanArchitectureResources.RepositoryTemplate}.template");

            // act
            handler.Execute();

            // assert
            fakes.IDirectory.Verify(x => x.Create(outputFolder), Times.Once);
            fakes.ITemplateInteractor.Verify(x => x.RenderAndSave(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()), Times.Exactly(allEntities.Count));

            foreach (Entity entity in allEntities)
            {
                string fullSavePath = Path.Combine(
                    fakes.CleanArchitectureExpander.Object.GetComponentOutputFolder(fakes.InfrastructureComponent.Object),
                    CleanArchitectureResources.RepositoryFolder,
                    $"{entity.Name}Repository.cs");

                fakes.ITemplateInteractor.Verify(
                    x => x.RenderAndSave(
                        expectedTemplateBaseBath,
                        It.Is<object>(x =>
                        x.GetHashCode() == new
                        {
                            entity,
                            component = fakes.InfrastructureComponent.Object,
                            applicationComponent = fakes.ApplicationComponent.Object,
                        }.GetHashCode()),
                        fullSavePath),
                    Times.Once);
            }
        }
    }
}
