using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Infrastructure;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
using Moq;
using Xunit;
using CleanArchitectureResources = LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Resources;

namespace LiquidVisions.PanthaRhei.Generator.CleanArchitecture.Tests.Handlers.Infrastructure
{
    public class ExpandRepositoryHandlerInteractorTests
    {
        private readonly ExpandRepositoryHandlerInteractor handler;
        private readonly string outputFolder;
        private readonly CleanArchitectureFakes fakes = new();
        private readonly List<Entity> allEntities;
        private readonly App app;

        public ExpandRepositoryHandlerInteractorTests()
        {
            allEntities = fakes.GetValidEntities();
            app = fakes.SetupApp(allEntities);
            fakes.IProjectAgentInteractor.Setup(x => x.GetComponentOutputFolder(fakes.InfrastructureComponent.Object)).Returns(fakes.ExpectedCompontentOutputFolder);
            fakes.MockCleanArchitectureExpander(allEntities);
            handler = new(fakes.CleanArchitectureExpander.Object, fakes.IDependencyFactoryInteractor.Object);

            outputFolder = Path.Combine(fakes.IProjectAgentInteractor.Object.GetComponentOutputFolder(fakes.InfrastructureComponent.Object), CleanArchitectureResources.RepositoryFolder);
        }

        [Fact]
        public void Constructor_ShouldValidate()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IProjectAgentInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ITemplateInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<Parameters>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<App>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(5));
        }

        [Fact]
        public void Constructor_ShouldCreateOutputDirectory()
        {
            // arrange
            // act
            // assert
            fakes.IDirectory.Verify(x => x.Create(It.IsAny<string>()), Times.Once);
            fakes.IDirectory.Verify(x => x.Create(outputFolder), Times.Once);
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
            string expectedTemplateBaseBath = Path.Combine(fakes.Parameters.Object.ExpandersFolder, fakes.CleanArchitectureExpander.Object.Model.Name, fakes.CleanArchitectureExpander.Object.Model.TemplateFolder, $"{CleanArchitectureResources.RepositoryTemplate}.template");

            // act
            handler.Execute();

            // assert
            fakes.ITemplateInteractor.Verify(x => x.RenderAndSave(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()), Times.Exactly(allEntities.Count));

            foreach (Entity entity in allEntities)
            {
                string fullSavePath = Path.Combine(
                    fakes.IProjectAgentInteractor.Object.GetComponentOutputFolder(fakes.InfrastructureComponent.Object),
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
