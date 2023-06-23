using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders;
using LiquidVisions.PanthaRhei.Generator.Application.RequestModels;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.Application.Tests.Interactors.Seeders
{
    public class ComponentSeederInteractorTests
    {
        private readonly Fakes fakes = new();
        private readonly ComponentSeederInteractor interactor;
        private readonly Mock<ICreateGateway<Component>> mockedCreateGateway = new();
        private readonly Mock<IDeleteGateway<Component>> mockedDeleteGateway = new();

        public ComponentSeederInteractorTests()
        {
            fakes.IDependencyFactoryInteractor.Setup(x => x.Get<ICreateGateway<Component>>()).Returns(mockedCreateGateway.Object);
            fakes.IDependencyFactoryInteractor.Setup(x => x.Get<IDeleteGateway<Component>>()).Returns(mockedDeleteGateway.Object);

            interactor = new ComponentSeederInteractor(fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Constructor_ShouldVerifyDependencies()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ICreateGateway<Component>>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IDeleteGateway<Component>>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IDirectory>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IFile>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(5));
            fakes.IDependencyFactoryInteractor.Verify(x => x.GetAll<It.IsAnyType>(), Times.Never);
        }

        [Fact]
        public void SeedOrder_ShouldBe1()
        {
            // arrange
            // act
            // assert
            Assert.Equal(3, interactor.SeedOrder);
        }

        [Fact]
        public void ResetOrder_ShouldBe1()
        {
            // arrange
            // act
            // assert
            Assert.Equal(3, interactor.ResetOrder);
        }

        [Fact]
        public void Reset_ShouldVerify()
        {
            // arrange
            // act
            interactor.Reset();

            // assert
            mockedDeleteGateway.Verify(x => x.DeleteAll(), Times.Once);
        }

        [Fact]
        public void Execute_ExpanderFolderDoesNotExist_ShouldNotCreate()
        {
            // arrange
            Expander expander1 = new() { Name = "Expander1", TemplateFolder = ".Templates" };
            Expander expander2 = new() { Name = "Expander2", TemplateFolder = ".Templates" };
            App app = new()
            {
                Expanders = new List<Expander> { expander1, expander2, },
            };

            string actualTemplatePathExpander1 = Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, expander1.Name, expander1.TemplateFolder);
            string actualTemplatePathExpander2 = Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, expander2.Name, expander2.TemplateFolder);

            fakes.IDirectory.Setup(x => x.Exists(actualTemplatePathExpander1)).Returns(false);
            fakes.IDirectory.Setup(x => x.Exists(actualTemplatePathExpander2)).Returns(false);

            // act
            interactor.Seed(app);

            // assert
            fakes.IDirectory.Verify(x => x.Exists(actualTemplatePathExpander1), Times.Once);
            fakes.IDirectory.Verify(x => x.Exists(actualTemplatePathExpander2), Times.Once);
            fakes.IDirectory.Verify(x => x.GetFiles(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<SearchOption>()), Times.Never);
            mockedCreateGateway.Verify(x => x.Create(It.IsAny<Component>()), Times.Never);
        }

        [Fact]
        public void Execute_HappyFlow_ShouldVerify()
        {
            // arrange
            Expander expander1 = new() { Name = "Expander1", TemplateFolder = ".Templates" };
            Expander expander2 = new() { Name = "Expander2", TemplateFolder = ".Templates" };
            App app = new()
            {
                Expanders = new List<Expander> { expander1, expander2, },
            };

            string actualTemplatePathExpander1 = Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, expander1.Name, expander1.TemplateFolder);
            string actualTemplatePathExpander2 = Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, expander2.Name, expander2.TemplateFolder);

            fakes.IDirectory.Setup(x => x.Exists(actualTemplatePathExpander1)).Returns(true);
            fakes.IDirectory.Setup(x => x.Exists(actualTemplatePathExpander2)).Returns(true);

            fakes.IDirectory.Setup(x => x.GetFiles(actualTemplatePathExpander1, "*.csproj", SearchOption.AllDirectories)).Returns(new string[] { $"{actualTemplatePathExpander1}\\NAME.Project1.csproj", string.Empty });
            fakes.IDirectory.Setup(x => x.GetFiles(actualTemplatePathExpander2, "*.csproj", SearchOption.AllDirectories)).Returns(new string[] { $"{actualTemplatePathExpander2}\\NAME.Project2.csproj", string.Empty });

            fakes.IFile.Setup(x => x.GetFileNameWithoutExtension($"{actualTemplatePathExpander1}\\NAME.Project1.csproj")).Returns("Project1");
            fakes.IFile.Setup(x => x.GetFileNameWithoutExtension($"{actualTemplatePathExpander2}\\NAME.Project2.csproj")).Returns("Project2");

            // act
            interactor.Seed(app);

            // assert
            fakes.IDirectory.Verify(x => x.GetFiles(actualTemplatePathExpander1, "*.csproj", SearchOption.AllDirectories), Times.Once);
            fakes.IDirectory.Verify(x => x.GetFiles(actualTemplatePathExpander2, "*.csproj", SearchOption.AllDirectories), Times.Once);
            mockedCreateGateway.Verify(x => x.Create(It.IsAny<Component>()), Times.Exactly(2));
        }
    }
}
