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
        private readonly Fakes _fakes = new();
        private readonly ComponentSeeder _interactor;
        private readonly Mock<ICreateRepository<Component>> _mockedCreateGateway = new();
        private readonly Mock<IDeleteRepository<Component>> _mockedDeleteGateway = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentSeederTests"/> class.
        /// </summary>
        public ComponentSeederTests()
        {
            _fakes.IDependencyFactory.Setup(x => x.Get<ICreateRepository<Component>>()).Returns(_mockedCreateGateway.Object);
            _fakes.IDependencyFactory.Setup(x => x.Get<IDeleteRepository<Component>>()).Returns(_mockedDeleteGateway.Object);

            _interactor = new ComponentSeeder(_fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Dependency tests.
        /// </summary>
        [Fact]
        public void ConstructorShouldVerifyDependencies()
        {
            // arrange
            // act
            // assert
            _fakes.IDependencyFactory.Verify(x => x.Get<ICreateRepository<Component>>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Get<IDeleteRepository<Component>>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Get<IDirectory>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Get<IFile>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(5));
            _fakes.IDependencyFactory.Verify(x => x.GetAll<It.IsAnyType>(), Times.Never);
        }

        /// <summary>
        /// Test for <seealso cref="ComponentSeeder.SeedOrder"/>.
        /// </summary>
        [Fact]
        public void SeedOrderShouldBe1()
        {
            // arrange
            // act
            // assert
            Assert.Equal(3, _interactor.SeedOrder);
        }


        /// <summary>
        /// Test for <seealso cref="ComponentSeeder.ResetOrder"/>.
        /// </summary>
        [Fact]
        public void ResetOrderShouldBe1()
        {
            // arrange
            // act
            // assert
            Assert.Equal(3, _interactor.ResetOrder);
        }


        /// <summary>
        /// Test for <seealso cref="ComponentSeeder.Reset"/>.
        /// </summary>
        [Fact]
        public void ResetShouldVerify()
        {
            // arrange
            // act
            _interactor.Reset();

            // assert
            _mockedDeleteGateway.Verify(x => x.DeleteAll(), Times.Once);
        }


        /// <summary>
        /// Test for <seealso cref="ComponentSeeder.Seed(App)"/> edge case scenario.
        /// </summary>
        [Fact]
        public void ExecuteExpanderFolderDoesNotExistShouldNotCreate()
        {
            // arrange
            Expander expander1 = new() { Name = "Expander1", Enabled = true };
            Expander expander2 = new() { Name = "Expander2", Enabled = true };
            App app = new()
            {
                Expanders = new List<Expander> { expander1, expander2, },
            };

            string actualTemplatePathExpander1 = Path.Combine(_fakes.GenerationOptions.Object.ExpandersFolder, expander1.Name, Resources.TemplatesFolder);
            string actualTemplatePathExpander2 = Path.Combine(_fakes.GenerationOptions.Object.ExpandersFolder, expander2.Name, Resources.TemplatesFolder);

            _fakes.IDirectory.Setup(x => x.Exists(actualTemplatePathExpander1)).Returns(false);
            _fakes.IDirectory.Setup(x => x.Exists(actualTemplatePathExpander2)).Returns(false);

            // act
            _interactor.Seed(app);

            // assert
            _fakes.IDirectory.Verify(x => x.Exists(actualTemplatePathExpander1), Times.Once);
            _fakes.IDirectory.Verify(x => x.Exists(actualTemplatePathExpander2), Times.Once);
            _fakes.IDirectory.Verify(x => x.GetFiles(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<SearchOption>()), Times.Never);
            _mockedCreateGateway.Verify(x => x.Create(It.IsAny<Component>()), Times.Never);
        }

        /// <summary>
        /// Test for <seealso cref="ComponentSeeder.Seed(App)"/> happyflow.
        /// </summary>
        [Fact]
        public void ExecuteHappyFlowShouldVerify()
        {
            // arrange
            Expander expander1 = new() { Name = "Expander1", Enabled = true };
            Expander expander2 = new() { Name = "Expander2", Enabled = true };
            App app = new()
            {
                Expanders = new List<Expander> { expander1, expander2, },
            };

            string actualTemplatePathExpander1 = Path.Combine(_fakes.GenerationOptions.Object.ExpandersFolder, expander1.Name, Resources.TemplatesFolder);
            string actualTemplatePathExpander2 = Path.Combine(_fakes.GenerationOptions.Object.ExpandersFolder, expander2.Name, Resources.TemplatesFolder);

            _fakes.IDirectory.Setup(x => x.Exists(actualTemplatePathExpander1)).Returns(true);
            _fakes.IDirectory.Setup(x => x.Exists(actualTemplatePathExpander2)).Returns(true);

            _fakes.IDirectory.Setup(x => x.GetFiles(actualTemplatePathExpander1, "*.csproj", SearchOption.AllDirectories)).Returns(new string[] { $"{actualTemplatePathExpander1}\\NAME.Project1.csproj", string.Empty });
            _fakes.IDirectory.Setup(x => x.GetFiles(actualTemplatePathExpander2, "*.csproj", SearchOption.AllDirectories)).Returns(new string[] { $"{actualTemplatePathExpander2}\\NAME.Project2.csproj", string.Empty });

            _fakes.IFile.Setup(x => x.GetFileNameWithoutExtension($"{actualTemplatePathExpander1}\\NAME.Project1.csproj")).Returns("Project1");
            _fakes.IFile.Setup(x => x.GetFileNameWithoutExtension($"{actualTemplatePathExpander2}\\NAME.Project2.csproj")).Returns("Project2");

            // act
            _interactor.Seed(app);

            // assert
            _fakes.IDirectory.Verify(x => x.GetFiles(actualTemplatePathExpander1, "*.csproj", SearchOption.AllDirectories), Times.Once);
            _fakes.IDirectory.Verify(x => x.GetFiles(actualTemplatePathExpander2, "*.csproj", SearchOption.AllDirectories), Times.Once);
            _mockedCreateGateway.Verify(x => x.Create(It.IsAny<Component>()), Times.Exactly(2));
        }
    }
}
