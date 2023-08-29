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
        private readonly InfrastructureFakes _fakes = new();
        private readonly HarvestSerializer _serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="HarvestSerializerTests"/> class.
        /// </summary>
        public HarvestSerializerTests()
        {
            _serializer = new HarvestSerializer(_fakes.IDependencyFactory.Object);
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
            _fakes.IDependencyFactory.Verify(x => x.Get<IFile>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Get<ISerializer<Harvest>>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Get<IDirectory>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(3));
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
            _fakes.IFile.Setup(x => x.Exists(filePath)).Returns(true);
            _fakes.IFile.Setup(x => x.GetDirectory(filePath)).Returns(folderPath);
            _fakes.IDirectory.Setup(x => x.Exists(folderPath)).Returns(folderExists);
            Harvest entity = new(Resources.RegionHarvesterExtensionFile)
            {
                Path = $"{folderPath}File.cs",
                Items = { new HarvestItem { Content = "Content", Tag = "Tag", } },
            };

            // act
            _serializer.Serialize(entity, filePath);

            // assert
            _fakes.ISerializer.Verify(x => x.Serialize(filePath, entity), Times.Once);
            _fakes.IDirectory.Verify(x => x.Exists(folderPath), Times.Once);
            _fakes.IDirectory.Verify(x => x.Create(folderPath), Times.Exactly(timesCreateFolder));
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
            _serializer.Serialize(entity, filePath);

            // assert
            _fakes.ISerializer.Verify(x => x.Serialize(filePath, entity), Times.Never);
        }
    }
}
