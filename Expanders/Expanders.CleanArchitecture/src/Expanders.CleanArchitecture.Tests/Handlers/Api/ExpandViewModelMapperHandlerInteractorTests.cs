using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api;
using Moq;
using Xunit;
using CleanArchitectureResources = LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Resources;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests.Handlers.Api
{
    /// <summary>
    /// Tests for <seealso cref="ExpandViewModelMapperTask"/>.
    /// </summary>
    public class ExpandViewModelMapperHandlerInteractorTests
    {
        private readonly CleanArchitectureFakes fakes = new ();
        private readonly ExpandViewModelMapperTask handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandViewModelMapperHandlerInteractorTests"/> class.
        /// </summary>
        public ExpandViewModelMapperHandlerInteractorTests()
        {
            fakes.MockCleanArchitectureExpander(new List<Entity> { fakes.ExpectedEntity });
            handler = new (fakes.CleanArchitectureExpander.Object, fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Dependency tests
        /// </summary>
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

        /// <summary>
        /// Tests for <seealso cref="ExpandViewModelMapperTask.Order"/>.
        /// </summary>
        [Fact]
        public void Order_ShouldValidate()
        {
            // arrange
            // act
            // assert
            Assert.Equal(15, handler.Order);
        }

        /// <summary>
        /// Tests for <seealso cref="ExpandViewModelMapperTask.Enabled"/>.
        /// </summary>
        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(nameof(ExpandViewModelMapperTask), handler.Name);
        }

        /// <summary>
        /// Test for <seealso cref="ExpandViewModelMapperTask.Enabled"/>.
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
        /// Test for <seealso cref="ExpandViewModelMapperTask.Enabled"/>.
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
        /// Tests for <seealso cref="ExpandViewModelMapperTask.Execute()"/>.
        /// </summary>
        [Fact]
        public void Execute_ShouldRenderAnSaveViewModelTemplate()
        {
            // arrange
            string expectedViewModelFolder = Path.Combine(fakes.ExpectedCompontentOutputFolder, CleanArchitectureResources.ViewModelMapperFolder);
            string expectedTemplatePath = Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, fakes.CleanArchitectureExpander.Object.Model.Name, PanthaRhei.Domain.Resources.TemplatesFolder, $"{CleanArchitectureResources.ViewModelMapperTemplate}.template");
            string expectedFilePath = Path.Combine(expectedViewModelFolder, $"{fakes.ExpectedEntity.Name}ViewModelMapper.cs");

            // act
            handler.Execute();

            // assert
            fakes.IDirectory.Verify(x => x.Create(expectedViewModelFolder), Times.Once);
            fakes.ITemplate.Verify(
                x => x.RenderAndSave(
                    expectedTemplatePath,
                    It.Is<object>(x => x.GetHashCode() == new
                    {
                        Entity = fakes.ExpectedEntity,
                        component = fakes.ApiComponent.Object,
                        applicationComponent = fakes.ApplicationComponent.Object,
                    }
                    .GetHashCode()), expectedFilePath), Times.Once);
        }
    }
}
