using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Infrastructure.Serialization;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Infrastructure.Tests
{
    public class HarvestSerializerInteractorTests
    {
        private readonly InfrastructureFakes fakes = new();
        private readonly HarvestSerializer serializer;

        public HarvestSerializerInteractorTests()
        {
            serializer = new HarvestSerializer(fakes.IDependencyFactory.Object);
        }

        [Fact]
        public void Dependencies_ShouldBeResolved()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Get<IFile>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<ISerializer<Harvest>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<IDirectory>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(3));
        }

        [Theory]
        [InlineData(true, 0)]
        [InlineData(false, 1)]
        public void Serialize_ShouldSerialize(bool folderExists, int timesCreateFolder)
        {
            // arrange
            string folderPath = "C:\\Some\\Full\\Path\\To\\";
            string filePath = $"{folderPath}File{Resources.RegionHarvesterExtensionFile}";
            fakes.IFile.Setup(x => x.Exists(filePath)).Returns(true);
            fakes.IFile.Setup(x => x.GetDirectory(filePath)).Returns(folderPath);
            fakes.IDirectory.Setup(x => x.Exists(folderPath)).Returns(folderExists);
            Harvest entity = new(Resources.RegionHarvesterExtensionFile)
            {
                Path = $"{folderPath}File.cs",
                Items = { new HarvestItem { Content = "Content", Tag = "Tag", } },
            };

            // act
            serializer.Serialize(entity, filePath);

            // assert
            fakes.ISerializerInteractor.Verify(x => x.Serialize(filePath, entity), Times.Once);
            fakes.IDirectory.Verify(x => x.Exists(folderPath), Times.Once);
            fakes.IDirectory.Verify(x => x.Create(folderPath), Times.Exactly(timesCreateFolder));
        }

        [Fact]
        public void Serialize_WithNoHarvestItems_ShouldNotSerialize()
        {
            // arrange
            string folderPath = "C:\\Some\\Full\\Path\\To\\";
            string filePath = $"{folderPath}File{Resources.RegionHarvesterExtensionFile}";
            Harvest entity = new(Resources.RegionHarvesterExtensionFile)
            {
                Path = $"{folderPath}File.cs",
            };

            // act
            serializer.Serialize(entity, filePath);

            // assert
            fakes.ISerializerInteractor.Verify(x => x.Serialize(filePath, entity), Times.Never);
        }
    }
}
