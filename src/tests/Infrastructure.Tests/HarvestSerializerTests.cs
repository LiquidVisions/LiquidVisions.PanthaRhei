using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Infrastructure.Serialization;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Infrastructure.Tests
{
    /// <summary>
    /// Tests for <see cref="HarvestSerializer"/>.
    /// </summary>
    public class HarvestSerializerTests
    {
        private readonly InfrastructureFakes fakes = new();
        private readonly HarvestSerializer serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="HarvestSerializerTests"/> class.
        /// </summary>
        public HarvestSerializerTests()
        {
            serializer = new HarvestSerializer(fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Dependencies should be resolved.
        /// </summary>
        [Fact]
        public void DependenciesShouldBeResolved()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Resolve<IFile>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<ISerializer<Harvest>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IDirectory>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(3));
        }

        /// <summary>
        /// Test for <see cref="HarvestSerializer.Serialize(Harvest, string)"/>.
        /// </summary>
        /// <param name="folderExists">A boolean indicating whether the folder exists.</param>
        /// <param name="timesCreateFolder">Number of times the folder should be created.</param>
        [Theory]
        [InlineData(true, 0)]
        [InlineData(false, 1)]
        public void SerializeShouldSerialize(bool folderExists, int timesCreateFolder)
        {
            // arrange
            string folderPath = "C:\\Some\\Full\\Path\\To\\";
            string filePath = $"{folderPath}File{Resources.RegionHarvesterExtensionFile}";
            fakes.IFile.Setup(x => x.Exists(filePath)).Returns(false);
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
            fakes.ISerializer.Verify(x => x.Serialize(filePath, entity), Times.Once);
            fakes.IDirectory.Verify(x => x.Exists(folderPath), Times.Once);
            fakes.IDirectory.Verify(x => x.Create(folderPath), Times.Exactly(timesCreateFolder));
        }

        /// <summary>
        /// Test for <see cref="HarvestSerializer.Serialize(Harvest, string)"/>.
        /// Should not be serialized if the file does not exist.
        /// </summary>
        [Fact]
        public void SerializeWithNoHarvestItemsShouldNotSerialize()
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
            fakes.ISerializer.Verify(x => x.Serialize(filePath, entity), Times.Never);
        }
    }
}
