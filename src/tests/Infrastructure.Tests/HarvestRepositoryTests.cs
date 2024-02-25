using System;
using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Infrastructure.Serialization;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Infrastructure.Tests
{
    /// <summary>
    /// Tests for <see cref="HarvestRepository"/>.
    /// </summary>
    public class HarvestRepositoryTests
    {
        private readonly HarvestRepository repository;
        private readonly InfrastructureFakes fakes = new();
        private readonly App app;

        /// <summary>
        /// Initializes a new instance of the <see cref="HarvestRepositoryTests"/> class.
        /// </summary>
        public HarvestRepositoryTests()
        {
            app = new()
            {
                FullName = "AppFullName",
            };
            fakes.IDependencyFactory.Setup(x => x.Resolve<App>()).Returns(app);
            repository = new HarvestRepository(fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Dependency injection test.
        /// </summary>
        [Fact]
        public void DependenciesShouldBeResolved()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Resolve<IFile>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IDeserializer<Harvest>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IHarvestSerializer>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<App>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(5));
        }

        /// <summary>
        /// Test for <see cref="HarvestRepository.Create(Harvest)"/>.
        /// </summary>
        [Fact]
        public void CreateShouldSerialize()
        {
            // arrange
            string extension = Resources.RegionHarvesterExtensionFile;
            string pathWithoutExtension = $"C:\\Full\\Path\\To\\Harvest\\File";
            string path = $"{pathWithoutExtension}.cs";
            fakes.IFile.Setup(x => x.GetFileNameWithoutExtension(path)).Returns(pathWithoutExtension);
            Harvest entity = new(extension)
            {
                Path = path,
            };

            string fullSavePath = Path.Combine(fakes.GenerationOptions.Object.HarvestFolder, app.FullName, $"{pathWithoutExtension}.{extension}");

            // act
            bool result = repository.Create(entity);

            // assert
            Assert.True(result);
            fakes.IHarvestSerializer.Verify(x => x.Serialize(entity, fullSavePath), Times.Once);
        }

        /// <summary>
        /// Test for <see cref="HarvestRepository.Create(Harvest)"/>.
        /// Should throw <seealso cref="InvalidProgramException"/> when harvest type is null."/>
        /// </summary>
        [Fact]
        public void CreateShouldThrowException()
        {
            // arrange
            Harvest harvest = new();

            // act
            // assert
            InvalidProgramException exception = Assert.Throws<InvalidProgramException>(() => repository.Create(harvest));
            Assert.Equal("Expected harvest type.", exception.Message);
        }

        /// <summary>
        /// Test for <see cref="HarvestRepository.GetAll"/>.
        /// Should throw <seealso cref="NotImplementedException"/> because it is not implemented.
        /// </summary>
        [Fact]
        public void GetAllShouldThrowException()
        {
            // arrange
            // act
            // assert
            Assert.Throws<NotImplementedException>(() => repository.GetAll());
        }

        /// <summary>
        /// Test for <see cref="HarvestRepository.GetById(object)"/>.
        /// Should deserialize harvest file.
        /// </summary>
        [Fact]
        public void GetByIdShouldDeserialize()
        {
            // arrange
            string somePath = $"C:\\Some\\To\\HarvestFile{Resources.RegionHarvesterExtensionFile}";
            fakes.IFile.Setup(x => x.Exists(somePath)).Returns(true);

            // act
            repository.GetById(somePath);

            // assert
            fakes.IFile.Verify(x => x.Exists(somePath), Times.Once);
            fakes.IHarvestDeserializer.Verify(x => x.Deserialize(somePath), Times.Once);
        }

        /// <summary>
        /// Test for <see cref="HarvestRepository.GetById(object)"/>.
        /// Should throw <seealso cref="FileNotFoundException"/> when file does not exist.
        /// </summary>
        [Fact]
        public void GetByIdShouldThrowException()
        {
            // arrange
            string somePath = $"C:\\Some\\To\\HarvestFile{Resources.RegionHarvesterExtensionFile}";
            fakes.IFile.Setup(x => x.Exists(somePath)).Returns(false);

            // act
            // assert
            FileNotFoundException exception = Assert.Throws<FileNotFoundException>(() => repository.GetById(somePath));
            Assert.Equal($"Harvest file not found on path {somePath}", exception.Message);
        }
    }
}
