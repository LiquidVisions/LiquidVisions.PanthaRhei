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
    /// Tests for <seealso cref="ExpandDatabaseContextTask"/>.
    /// </summary>
    public class ExpandDatabaseContextHandlerInteractorTests
    {
        private readonly ExpandDatabaseContextTask handler;
        private readonly CleanArchitectureFakes fakes = new ();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandDatabaseContextHandlerInteractorTests"/> class.
        /// </summary>
        public ExpandDatabaseContextHandlerInteractorTests()
        {
            fakes.MockCleanArchitectureExpander(new List<Entity> { fakes.ExpectedEntity });
            handler = new (fakes.CleanArchitectureExpander.Object, fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Tests dependencies used on <seealso cref="ExpandDatabaseContextTask"/>.
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
            fakes.IDependencyFactory.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(3));
        }

        /// <summary>
        /// Test for <seealso cref="ExpandDatabaseContextTask.Order"/>.
        /// </summary>
        [Fact]
        public void Order_ShouldValidate()
        {
            // arrange
            // act
            // assert
            Assert.Equal(9, handler.Order);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(nameof(ExpandDatabaseContextTask), handler.Name);
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
        public void Execute_ShouldCreateAndSaveTemplate()
        {
            // arrange
            string expectedTemplateBaseBath = Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, fakes.CleanArchitectureExpander.Object.Model.Name, PanthaRhei.Domain.Resources.TemplatesFolder, $"{CleanArchitectureResources.DbContextTemplate}.template");
            string expectedSavePath = Path.Combine(fakes.ExpectedCompontentOutputFolder, "Context.cs");
            var expectedListOfEntities = new List<Entity> { fakes.ExpectedEntity };
            App app = fakes.SetupApp(expectedListOfEntities);

            // act
            handler.Execute();

            // assert
            Assert.Equal(expectedListOfEntities, app.Entities);

            Assert.Equal(new { entities = expectedListOfEntities }.GetHashCode(), new { entities = app.Entities }.GetHashCode());

            fakes.ITemplate.Verify(x => x.RenderAndSave(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()), Times.Once);
            fakes.ITemplate.Verify(
                x => x.RenderAndSave(
                    expectedTemplateBaseBath,
                    It.IsAny<object>(), // Bug in the system where a collection in anonymous object is not correctly validated.
                    expectedSavePath),
                Times.Once);
        }
    }
}
