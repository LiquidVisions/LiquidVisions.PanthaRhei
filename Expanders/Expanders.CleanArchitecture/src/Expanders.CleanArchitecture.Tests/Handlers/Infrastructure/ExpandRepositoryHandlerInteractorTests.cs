using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Infrastructure;
using Moq;
using Xunit;
using CleanArchitectureResources = LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Resources;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests.Handlers.Infrastructure
{
    /// <summary>
    /// Tests for <seealso cref="ExpandRepositoryTask"/>.
    /// </summary>
    public class ExpandRepositoryHandlerInteractorTests
    {
        private readonly ExpandRepositoryTask handler;
        private readonly string outputFolder;
        private readonly CleanArchitectureFakes fakes = new ();
        private readonly List<Entity> allEntities;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandRepositoryHandlerInteractorTests"/> class.
        /// </summary>
        public ExpandRepositoryHandlerInteractorTests()
        {
            allEntities = fakes.GetValidEntities();
            fakes.MockCleanArchitectureExpander(allEntities);
            handler = new(fakes.CleanArchitectureExpander.Object, fakes.IDependencyFactory.Object);

            outputFolder = Path.Combine(fakes.CleanArchitectureExpander.Object.GetComponentOutputFolder(fakes.InfrastructureComponent.Object), CleanArchitectureResources.RepositoryFolder);
        }

        /// <summary>
        /// Tests dependencies on <seealso cref="ExpandRepositoryTask"/>.
        /// </summary>
        [Fact]
        public void Constructor_ShouldValidate()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Get<ITemplate>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<App>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(4));
        }

        /// <summary>
        /// Tests <seealso cref="ExpandRepositoryTask.Order"/>.
        /// </summary>
        [Fact]
        public void Order_ShouldValidate()
        {
            // arrange
            // act
            // assert
            Assert.Equal(11, handler.Order);
        }

        /// <summary>
        /// Tests <seealso cref="ExpandRepositoryTask.Name"/>.
        /// </summary>
        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(nameof(ExpandRepositoryTask), handler.Name);
        }

        /// <summary>
        /// Tests for <seealso cref="ExpandRepositoryTask.Enabled"/>.
        /// </summary>
        /// <param name="mode"><seealso cref="GenerationModes"/>.</param>
        /// <param name="expectedResult">Indicates the expected result.</param>
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

        /// <summary>
        /// Tests for <seealso cref="ExpandRepositoryTask.Enabled"/> 2nd scenario.
        /// </summary>
        /// <param name="clean">Indicates if the task should clean.</param>
        /// <param name="expectedResult">Indicates the expected result.</param>
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

        /// <summary>
        /// Test for <seealso cref="ExpandRepositoryTask.Execute"/>
        /// </summary>
        [Fact]
        public void Execute_ShouldCreateAndSaveTemplate()
        {
            // arrange
            string expectedTemplateBaseBath = Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, fakes.CleanArchitectureExpander.Object.Model.Name, fakes.CleanArchitectureExpander.Object.Model.TemplateFolder, $"{CleanArchitectureResources.RepositoryTemplate}.template");

            // act
            handler.Execute();

            // assert
            fakes.IDirectory.Verify(x => x.Create(outputFolder), Times.Once);
            fakes.ITemplate.Verify(x => x.RenderAndSave(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()), Times.Exactly(allEntities.Count));

            foreach (Entity entity in allEntities)
            {
                string fullSavePath = Path.Combine(
                    fakes.CleanArchitectureExpander.Object.GetComponentOutputFolder(fakes.InfrastructureComponent.Object),
                    CleanArchitectureResources.RepositoryFolder,
                    $"{entity.Name}Repository.cs");

                fakes.ITemplate.Verify(
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
