using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Client;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.CleanArchitecture.Tests.Handlers.Clients
{
    public class AddViewModelsTests
    {
        private readonly AddViewModels handler;
        private readonly CleanArchitectureFakes fakes = new();
        private readonly string expectedCreateFolder;

        public AddViewModelsTests()
        {
            expectedCreateFolder = Path.Combine(fakes.ExpectedCompontentOutputFolder, Resources.ViewModelsFolder);

            fakes.IProjectAgentInteractor.Setup(x => x.GetComponentOutputFolder(fakes.ClientComponent.Object)).Returns(fakes.ExpectedCompontentOutputFolder);
            fakes.MockCleanArchitectureExpander(new List<Entity> { fakes.ExpectedEntity });
            handler = new(fakes.CleanArchitectureExpander.Object, fakes.IDependencyFactoryInteractor.Object);
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

            fakes.IDirectory.Verify(x => x.Create(expectedCreateFolder), Times.Once);
        }

        [Fact]
        public void Order_ShouldValidate()
        {
            // arrange
            // act
            // assert
            Assert.Equal(17, handler.Order);
        }

        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(nameof(AddViewModels), handler.Name);
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
        public void Execute_ShouldExecuteAndSaveTemplate()
        {
            // arrange
            string expectedTemplateBaseBath = Path.Combine(fakes.Parameters.Object.ExpandersFolder, fakes.CleanArchitectureExpander.Object.Model.Name, fakes.CleanArchitectureExpander.Object.Model.TemplateFolder);

            // act
            handler.Execute();

            // assert

            fakes.ITemplateInteractor.Verify(x => x.RenderAndSave(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()), Times.Exactly(1));
            fakes.ITemplateInteractor.Verify(
                x => x.RenderAndSave(
                    Path.Combine(expectedTemplateBaseBath, $"{Resources.ViewModelTemplate}.template"),
                    It.Is<object>(x => x.GetHashCode() == new
                    {
                        component = fakes.ClientComponent.Object,
                        Entity = fakes.ExpectedEntity,
                    }.GetHashCode()),
                    Path.Combine(expectedCreateFolder, $"{fakes.ExpectedEntity.Name}ViewModel.cs")),
                Times.Once);
        }
    }
}
