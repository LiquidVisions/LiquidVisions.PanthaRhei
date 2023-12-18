using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api;
using Moq;
using Xunit;
using CleanArchitectureResources = LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Resources;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests.Handlers.Api
{
    /// <summary>
    /// Tests for <seealso cref="ExpandPresentersTask"/>
    /// </summary>
    public class ExpandPresentersHandlerInteractorTests
    {
        private readonly CleanArchitectureFakes fakes = new ();
        private readonly ExpandPresentersTask handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandPresentersHandlerInteractorTests"/> class.
        /// </summary>
        public ExpandPresentersHandlerInteractorTests()
        {
            fakes.CleanArchitectureExpander.Setup(x => x.GetComponentOutputFolder(fakes.ApiComponent.Object)).Returns(fakes.ExpectedCompontentOutputFolder);
            fakes.MockCleanArchitectureExpander(new List<Entity> { fakes.ExpectedEntity });
            handler = new (fakes.CleanArchitectureExpander.Object, fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Dependency tests.
        /// </summary>
        [Fact]
        public void Constructor_ShouldValidate()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<App>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IDirectory>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<ITemplate>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(4));
        }

        /// <summary>
        /// Test for <seealso cref="ExpandPresentersTask.Order"/>.
        /// </summary>
        [Fact]
        public void Order_ShouldValidate()
        {
            // arrange
            // act
            // assert
            Assert.Equal(14, handler.Order);
        }

        /// <summary>
        /// Test for <seealso cref="ExpandPresentersTask.Name"/>.
        /// </summary>
        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(nameof(ExpandPresentersTask), handler.Name);
        }

        /// <summary>
        /// Test for <seealso cref="ExpandPresentersTask.Enabled"/>.
        /// </summary>
        /// <param name="mode"><seealso cref="GenerationOptions"/>.</param>
        /// <param name="expectedResult">The expected result.</param>
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
        /// Test for <seealso cref="ExpandPresentersTask.Enabled"/>.
        /// </summary>
        /// <param name="clean">Tests with the Clean <seealso cref="GenerationOptions"/> mode.</param>
        /// <param name="expectedResult">The expected result.</param>
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
        /// Tests for <seealso cref="ExpandPresentersTask.Execute()"/>.
        /// </summary>
        [Fact]
        public void Execute_ShouldRenderAndSaveTemplate()
        {
            // arrange
            string expectedTemplatePath = Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, fakes.CleanArchitectureExpander.Object.Model.Name, PanthaRhei.Domain.Resources.TemplatesFolder, $"{CleanArchitectureResources.PresenterTemplate}.template");
            string expectedCreateFolder = Path.Combine(fakes.ExpectedCompontentOutputFolder, CleanArchitectureResources.PresentersFolder, fakes.ExpectedEntity.Name.Pluralize());
            string[] expectedActions = CleanArchitectureResources.DefaultRequestActions.Split(',', System.StringSplitOptions.TrimEntries);

            // act
            handler.Execute();

            // assert
            fakes.IDirectory.Verify(x => x.Create(expectedCreateFolder), Times.Once);
            fakes.ITemplate.Verify(x => x.RenderAndSave(expectedTemplatePath, It.IsAny<object>(), It.IsAny<string>()), Times.Exactly(5));
            foreach (string expectedAction in expectedActions)
            {
                fakes.ITemplate.Verify(
                x => x.RenderAndSave(
                    expectedTemplatePath,
                    It.Is<object>(x => x.GetHashCode() == new
                    {
                        applicationComponent = fakes.ApplicationComponent.Object,
                        component = fakes.ApiComponent.Object,
                        action = expectedAction,
                        entity = fakes.ExpectedEntity,
                    }.GetHashCode()),
                    Path.Combine(expectedCreateFolder, $"{fakes.ExpectedEntity.ToFileName(expectedAction, "Presenter")}.cs")),
                Times.Exactly(1));
            }
        }
    }
}
