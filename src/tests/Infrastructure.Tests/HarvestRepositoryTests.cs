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
    public class HarvestRepositoryTests
    {
        private readonly HarvestRepository repository;
        private readonly InfrastructureFakes fakes = new();
        private readonly App app;

        public HarvestRepositoryTests()
        {
            app = new()
            {
                FullName = "AppFullName",
            };
            fakes.IDependencyFactory.Setup(x => x.Get<App>()).Returns(app);
            repository = new HarvestRepository(fakes.IDependencyFactory.Object);
        }

        [Fact]
        public void Dependencies_ShouldBeResolved()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Get<IFile>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<IDeserializer<Harvest>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<IHarvestSerializer>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<App>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(5));
        }

        [Fact]
        public void Create_ShouldSerialize()
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

        [Fact]
        public void Create_ShouldThrowException()
        {
            // arrange
            Harvest harvest = new();

            // act
            // assert
            InvalidProgramException exception = Assert.Throws<InvalidProgramException>(() => repository.Create(harvest));
            Assert.Equal("Expected harvest type.", exception.Message);
        }

        [Fact]
        public void GetAll_ShouldThrowException()
        {
            // arrange
            // act
            // assert
            Assert.Throws<NotImplementedException>(() => repository.GetAll());
        }

        [Fact]
        public void GetById_ShouldDeserialize()
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

        [Fact]
        public void GetById_ShouldThrowException()
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
