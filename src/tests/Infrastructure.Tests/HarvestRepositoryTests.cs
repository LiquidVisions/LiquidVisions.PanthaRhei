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
        private readonly HarvestRepository _repository;
        private readonly InfrastructureFakes _fakes = new();
        private readonly App _app;

        /// <summary>
        /// Initializes a new instance of the <see cref="HarvestRepositoryTests"/> class.
        /// </summary>
        public HarvestRepositoryTests()
        {
            _app = new()
            {
                FullName = "AppFullName",
            };
            _fakes.IDependencyFactory.Setup(x => x.Resolve<App>()).Returns(_app);
            _repository = new HarvestRepository(_fakes.IDependencyFactory.Object);
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
            _fakes.IDependencyFactory.Verify(x => x.Resolve<IFile>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<IDeserializer<Harvest>>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<IHarvestSerializer>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<App>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(5));
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
            _fakes.IFile.Setup(x => x.GetFileNameWithoutExtension(path)).Returns(pathWithoutExtension);
            Harvest entity = new(extension)
            {
                Path = path,
            };

            string fullSavePath = Path.Combine(_fakes.GenerationOptions.Object.HarvestFolder, _app.FullName, $"{pathWithoutExtension}.{extension}");

            // act
            bool result = _repository.Create(entity);

            // assert
            Assert.True(result);
            _fakes.IHarvestSerializer.Verify(x => x.Serialize(entity, fullSavePath), Times.Once);
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
            InvalidProgramException exception = Assert.Throws<InvalidProgramException>(() => _repository.Create(harvest));
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
            Assert.Throws<NotImplementedException>(() => _repository.GetAll());
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
            _fakes.IFile.Setup(x => x.Exists(somePath)).Returns(true);

            // act
            _repository.GetById(somePath);

            // assert
            _fakes.IFile.Verify(x => x.Exists(somePath), Times.Once);
            _fakes.IHarvestDeserializer.Verify(x => x.Deserialize(somePath), Times.Once);
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
            _fakes.IFile.Setup(x => x.Exists(somePath)).Returns(false);

            // act
            // assert
            FileNotFoundException exception = Assert.Throws<FileNotFoundException>(() => _repository.GetById(somePath));
            Assert.Equal($"Harvest file not found on path {somePath}", exception.Message);
        }
    }
}
