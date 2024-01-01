using System;
using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Tests.Mocks;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Rejuvenators;
using LiquidVisions.PanthaRhei.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Domain.Tests.UseCases.Generators
{
    /// <summary>
    /// Tests the <see cref="RegionRejuvenator{TExpander}"/> class.
    /// </summary>
    public class RegionRejuvenatorTests
    {
        private readonly Fakes _fakes = new();
        private readonly RegionRejuvenator<FakeExpander> _rejuvenator;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionRejuvenatorTests"/> class.
        /// </summary>
        public RegionRejuvenatorTests()
        {
            App app = new()
            {
                FullName = "LiquidVisions.PanthaRhei.Tests",
            };


            _fakes.IDependencyFactory.Setup(x => x.Resolve<App>()).Returns(app);
            _rejuvenator = new RegionRejuvenator<FakeExpander>(_fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Tests that the constructor verifies dependencies.
        /// </summary>
        [Fact]
        public void ConstructorShouldVerifyDependencies()
        {
            // arrange
            // act
            // assert
            _fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<IDirectory>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<IGetRepository<Harvest>>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<IWriter>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<App>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<FakeExpander>(), Times.Once);

            _fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(6));
        }

        /// <summary>
        /// Tests ArgumentNullException in Object Constructor.
        /// </summary>
        [Fact]
        public void AssertThrowsArgumentNullException()
        {
            // arrange

            // act
            // assert
            Assert.Throws<ArgumentNullException>(() => new RegionRejuvenator<FakeExpander>(null));
        }

        /// <summary>
        /// Tests for <see cref="RegionRejuvenator{TExpander}.Enabled"/>.
        /// </summary>
        /// <param name="mode"><seealso cref="GenerationModes"/></param>
        /// <param name="folderExists">boolean to mock _directory.Exists()</param>
        /// <param name="expectedResult">Expected result</param>
        [Theory]
        [InlineData(GenerationModes.Deploy, false, false)]
        [InlineData(GenerationModes.Rejuvenate, false, false)]
        [InlineData(GenerationModes.Migrate, true, false)]
        [InlineData(GenerationModes.Rejuvenate, true, true)]
        public void ShouldVerifyEnabledProperty(GenerationModes mode, bool folderExists, bool expectedResult)
        {
            // arrange
            _fakes.GenerationOptions.Setup(x => x.Modes).Returns(mode);
            _fakes.IDirectory.Setup(x => x.Exists(It.IsAny<string>())).Returns(folderExists);

            // act
            // assert
            Assert.Equal(expectedResult, _rejuvenator.Enabled);
        }

        /// <summary>
        /// Tests for <see cref="RegionRejuvenator{TExpander}"/> Expander property.
        /// </summary>
        [Fact]
        public void ExpanderPropertyShouldReturnResolvedExpander()
        {
            // arrange
            FakeExpander expander = new Mock<FakeExpander>().Object;
            _fakes.IDependencyFactory.Setup(x => x.Resolve<FakeExpander>()).Returns(expander);
            RegionRejuvenator<FakeExpander> rejuvenator = new(_fakes.IDependencyFactory.Object);

            // act
            // assert
            Assert.Equal(expander, rejuvenator.Expander);
        }

        /// <summary>
        /// Rests for <see cref="RegionRejuvenator{TExpander}.Execute"/>.
        /// </summary>
        [Fact]
        public void ExtensionShouldReturnRegionHarvesterExtensionFile()
        {
            // arrange
            string harvestFile1 = $"C:/Path/To/Harvest/File.{Resources.RegionHarvesterExtensionFile}";
            Harvest harvest = new()
            {
                Path = harvestFile1,
                Items = [new HarvestItem { Tag = "tag", Content = "content" }]
            };
            _fakes.IDirectory.Setup(x => x.GetFiles(It.IsAny<string>(), $"*.{Resources.RegionHarvesterExtensionFile}", SearchOption.TopDirectoryOnly)).Returns([harvestFile1]);
            Mock<IGetRepository<Harvest>> mockedIGetRepository = new();
            mockedIGetRepository.Setup(x => x.GetById(harvestFile1)).Returns(harvest);
            _fakes.IDependencyFactory.Setup(x => x.Resolve<IGetRepository<Harvest>>()).Returns(mockedIGetRepository.Object);
            RegionRejuvenator<FakeExpander> rejuvenator = new(_fakes.IDependencyFactory.Object);

            // act
            rejuvenator.Execute();

            // assert
            _fakes.IDirectory.Verify(x => x.GetFiles(It.IsAny<string>(), $"*.{Resources.RegionHarvesterExtensionFile}", SearchOption.TopDirectoryOnly), Times.Once);
            mockedIGetRepository.Verify(x => x.GetById(harvestFile1), Times.Once);
            _fakes.IWriter.Verify(x => x.Load(harvestFile1), Times.Once);
            _fakes.IWriter.Verify(x => x.AddBetween($"#region ns-custom-{harvest.Items.Single().Tag}", $"#endregion ns-custom-{harvest.Items.Single().Tag}", $"{harvest.Items.Single().Content}"), Times.Once);
        }
    }
}
