using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Application.Usecases.Seeders;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Usecases.Seeders
{
    /// <summary>
    /// Tests for the <seealso cref="ComponentSeeder"/>.
    /// </summary>
    public class ComponentSeederTests
    {
        private readonly Fakes fakes = new();
        private readonly ComponentSeeder interactor;
        private readonly Mock<ICreateRepository<Component>> mockedCreateGateway = new();
        private readonly Mock<IDeleteRepository<Component>> mockedDeleteGateway = new();

        public ComponentSeederTests()
        {
            fakes.IDependencyFactory.Setup(x => x.Get<ICreateRepository<Component>>()).Returns(mockedCreateGateway.Object);
            fakes.IDependencyFactory.Setup(x => x.Get<IDeleteRepository<Component>>()).Returns(mockedDeleteGateway.Object);

            interactor = new ComponentSeeder(fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Dependency tests.
        /// </summary>
        [Fact]
        public void Constructor_ShouldVerifyDependencies()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Get<ICreateRepository<Component>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<IDeleteRepository<Component>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<IDirectory>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<IFile>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(5));
            fakes.IDependencyFactory.Verify(x => x.GetAll<It.IsAnyType>(), Times.Never);
        }

        /// <summary>
        /// Test for <seealso cref="ComponentSeeder.SeedOrder"/>.
        /// </summary>
        [Fact]
        public void SeedOrder_ShouldBe1()
        {
            // arrange
            // act
            // assert
            Assert.Equal(3, interactor.SeedOrder);
        }


        /// <summary>
        /// Test for <seealso cref="ComponentSeeder.ResetOrder"/>.
        /// </summary>
        [Fact]
        public void ResetOrder_ShouldBe1()
        {
            // arrange
            // act
            // assert
            Assert.Equal(3, interactor.ResetOrder);
        }


        /// <summary>
        /// Test for <seealso cref="ComponentSeeder.Reset"/>.
        /// </summary>
        [Fact]
        public void Reset_ShouldVerify()
        {
            // arrange
            // act
            interactor.Reset();

            // assert
            mockedDeleteGateway.Verify(x => x.DeleteAll(), Times.Once);
        }


        /// <summary>
        /// Test for <seealso cref="ComponentSeeder.Seed(App)"/> edge case scenario.
        /// </summary>
        [Fact]
        public void Execute_ExpanderFolderDoesNotExist_ShouldNotCreate()
        {
            // arrange
            Expander expander1 = new() { Name = "Expander1", Enabled = true };
            Expander expander2 = new() { Name = "Expander2", Enabled = true };
            App app = new()
            {
                Expanders = new List<Expander> { expander1, expander2, },
            };

            string actualTemplatePathExpander1 = Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, expander1.Name, Resources.TemplatesFolder);
            string actualTemplatePathExpander2 = Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, expander2.Name, Resources.TemplatesFolder);

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

        /// <summary>
        /// Test for <seealso cref="ComponentSeeder.Seed(App)"/> happyflow.
        /// </summary>
        [Fact]
        public void Execute_HappyFlow_ShouldVerify()
        {
            // arrange
            Expander expander1 = new() { Name = "Expander1", Enabled = true };
            Expander expander2 = new() { Name = "Expander2", Enabled = true };
            App app = new()
            {
                Expanders = new List<Expander> { expander1, expander2, },
            };

            string actualTemplatePathExpander1 = Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, expander1.Name, Resources.TemplatesFolder);
            string actualTemplatePathExpander2 = Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, expander2.Name, Resources.TemplatesFolder);

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
