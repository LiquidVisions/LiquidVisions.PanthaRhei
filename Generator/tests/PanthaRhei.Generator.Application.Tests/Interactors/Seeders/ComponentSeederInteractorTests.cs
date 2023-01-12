using System;
using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.Application.Tests.Interactors.Seeders
{
    public class ComponentSeederInteractorTests : AbstractTests
    {
        private readonly ComponentSeederInteractor interactor;
        private readonly Mock<ICreateGateway<Component>> mockedCreateGateway = new();
        private readonly Mock<IDeleteGateway<Component>> mockedDeleteGateway = new();

        public ComponentSeederInteractorTests()
        {
            Fakes.IDependencyFactoryInteractor.Setup(x => x.Get<ICreateGateway<Component>>()).Returns(mockedCreateGateway.Object);
            Fakes.IDependencyFactoryInteractor.Setup(x => x.Get<IDeleteGateway<Component>>()).Returns(mockedDeleteGateway.Object);

            interactor = new ComponentSeederInteractor(Fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Constructor_ShouldVerifyDependencies()
        {
            // arrange
            // act
            // assert
            Fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ICreateGateway<Component>>(), Times.Once);
            Fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IDeleteGateway<Component>>(), Times.Once);
            Fakes.IDependencyFactoryInteractor.Verify(x => x.Get<Parameters>(), Times.Once);
            Fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IDirectory>(), Times.Once);
            Fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IFile>(), Times.Once);
            Fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(5));
            Fakes.IDependencyFactoryInteractor.Verify(x => x.GetAll<It.IsAnyType>(), Times.Never);
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
            Expander expander1 = new Expander { Name = "Expander1", TemplateFolder = ".Templates" };
            Expander expander2 = new Expander { Name = "Expander2", TemplateFolder = ".Templates" };
            App app = new()
            {
                Expanders = new List<Expander> { expander1, expander2, },
            };

            string actualTemplatePathExpander1 = Path.Combine(Fakes.Parameters.Object.ExpandersFolder, expander1.Name, expander1.TemplateFolder);
            string actualTemplatePathExpander2 = Path.Combine(Fakes.Parameters.Object.ExpandersFolder, expander2.Name, expander2.TemplateFolder);

            Fakes.IDirectory.Setup(x => x.Exists(actualTemplatePathExpander1)).Returns(false);
            Fakes.IDirectory.Setup(x => x.Exists(actualTemplatePathExpander2)).Returns(false);

            // act
            interactor.Seed(app);

            // assert
            Fakes.IDirectory.Verify(x => x.Exists(actualTemplatePathExpander1), Times.Once);
            Fakes.IDirectory.Verify(x => x.Exists(actualTemplatePathExpander2), Times.Once);
            Fakes.IDirectory.Verify(x => x.GetFiles(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<SearchOption>()), Times.Never);
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

            string actualTemplatePathExpander1 = Path.Combine(Fakes.Parameters.Object.ExpandersFolder, expander1.Name, expander1.TemplateFolder);
            string actualTemplatePathExpander2 = Path.Combine(Fakes.Parameters.Object.ExpandersFolder, expander2.Name, expander2.TemplateFolder);

            Fakes.IDirectory.Setup(x => x.Exists(actualTemplatePathExpander1)).Returns(true);
            Fakes.IDirectory.Setup(x => x.Exists(actualTemplatePathExpander2)).Returns(true);

            Fakes.IDirectory.Setup(x => x.GetFiles(actualTemplatePathExpander1, "*.csproj", SearchOption.AllDirectories)).Returns(new string[] { $"{actualTemplatePathExpander1}\\NAME.Project1.csproj", string.Empty });
            Fakes.IDirectory.Setup(x => x.GetFiles(actualTemplatePathExpander2, "*.csproj", SearchOption.AllDirectories)).Returns(new string[] { $"{actualTemplatePathExpander2}\\NAME.Project2.csproj", string.Empty });

            Fakes.IFile.Setup(x => x.GetFileNameWithoutExtension($"{actualTemplatePathExpander1}\\NAME.Project1.csproj")).Returns("Project1");
            Fakes.IFile.Setup(x => x.GetFileNameWithoutExtension($"{actualTemplatePathExpander2}\\NAME.Project2.csproj")).Returns("Project2");

            // act
            interactor.Seed(app);

            // assert
            Fakes.IDirectory.Verify(x => x.GetFiles(actualTemplatePathExpander1, "*.csproj", SearchOption.AllDirectories), Times.Once);
            Fakes.IDirectory.Verify(x => x.GetFiles(actualTemplatePathExpander2, "*.csproj", SearchOption.AllDirectories), Times.Once);
            mockedCreateGateway.Verify(x => x.Create(It.IsAny<Component>()), Times.Exactly(2));
        }
    }
}
