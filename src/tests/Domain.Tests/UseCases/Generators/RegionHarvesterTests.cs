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
    /// Tests the <see cref="RegionHarvester{TExpander}"/> class.
    /// </summary>
    public class RegionHarvesterTests
    {
        private readonly Fakes fakes = new();
        private readonly RegionHarvester<FakeExpander> harvester;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionRejuvenatorTests"/> class.
        /// </summary>
        public RegionHarvesterTests()
        {
            App app = new()
            {
                FullName = "LiquidVisions.PanthaRhei.Tests",
            };


            fakes.IDependencyFactory.Setup(x => x.Resolve<App>()).Returns(app);
            harvester = new RegionHarvester<FakeExpander>(fakes.IDependencyFactory.Object);
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
            fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IDirectory>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IFile>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<ICreateRepository<Harvest>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<FakeExpander>(), Times.Once);
            
            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(5));
        }

        /// <summary>
        /// Tests ArgumentNullException in Object Constructor.
        /// </summary>
        [Fact]
        public void ThrowsNullReferenceException()
        {
            // arrange

            // act
            // assert
            Assert.Throws<NullReferenceException>(() => new RegionHarvester<FakeExpander>(null));
        }

        /// <summary>
        /// Tests for <see cref="RegionRejuvenator{TExpander}.Enabled"/>.
        /// </summary>
        /// <param name="mode"><seealso cref="GenerationModes"/></param>
        /// <param name="folderExists">boolean to mock directory.Exists()</param>
        /// <param name="expectedResult">Expected result</param>
        [Theory]
        [InlineData(GenerationModes.Deploy, false, false)]
        [InlineData(GenerationModes.Harvest, false, false)]
        [InlineData(GenerationModes.Migrate, true, false)]
        [InlineData(GenerationModes.Harvest, true, true)]
        public void ShouldVerifyEnabledProperty(GenerationModes mode, bool folderExists, bool expectedResult)
        {
            // arrange
            fakes.GenerationOptions.Setup(x => x.Modes).Returns(mode);
            fakes.IDirectory.Setup(x => x.Exists(It.IsAny<string>())).Returns(folderExists);

            // act
            // assert
            Assert.Equal(expectedResult, harvester.Enabled);
        }

        /// <summary>
        /// Tests for <see cref="RegionHarvester{TExpander}.Expander"/>.
        /// </summary>
        [Fact]
        public void ExpanderPropertyShouldReturnResolvedExpander()
        {
            // arrange
            FakeExpander expander = new Mock<FakeExpander>().Object;
            fakes.IDependencyFactory.Setup(x => x.Resolve<FakeExpander>()).Returns(expander);
            RegionHarvester<FakeExpander> rejuvenator = new(fakes.IDependencyFactory.Object);

            // act
            // assert
            Assert.Equal(expander, rejuvenator.Expander);
        }

        /// <summary>
        /// Rests for <see cref="RegionHarvester{TExpander}.Execute"/>.
        /// </summary>
        [Fact]
        public void HarvestItemsShouldBeCreatedAsExpected()
        {
            // arrange
            Mock<ICreateRepository<Harvest>> mockedCreateRepository = new();
            fakes.IDependencyFactory.Setup(x => x.Resolve<ICreateRepository<Harvest>>()).Returns(mockedCreateRepository.Object);

            string[] filePaths = ["C:/Path/file1.cs", "C:/Path/file2.cs", "C:/Path/file3.cs"];

            fakes.IDirectory.Setup(x => x.GetFiles(fakes.GenerationOptions.Object.OutputFolder, "*.cs", SearchOption.AllDirectories)).Returns(filePaths);
            fakes.IFile.Setup(x => x.ReadAllText(filePaths[0])).Returns(
@"#region ns-custom-test1
contentpart1
#endregion ns-custom-test1");
            fakes.IFile.Setup(x => x.ReadAllText(filePaths[1])).Returns(
@"  #region ns-custom-test2
    contentpart2
    #endregion ns-custom-test2");

            fakes.IFile.Setup(x => x.ReadAllText(filePaths[2])).Returns(
@"#region ns-custom-test3
#endregion ns-custom-test3");
            RegionHarvester<FakeExpander> harvester = new(fakes.IDependencyFactory.Object);

            // act
            harvester.Execute();

            // assert
            fakes.IDirectory.Verify(x => x.GetFiles(fakes.GenerationOptions.Object.OutputFolder, "*.cs", SearchOption.AllDirectories), Times.Once);
            fakes.IFile.Verify(x => x.ReadAllText(It.IsAny<string>()), Times.Exactly(3));
            mockedCreateRepository.Verify(x => x.Create(It.IsAny<Harvest>()), Times.Exactly(3));

            mockedCreateRepository.Verify(x => x.Create(It.Is<Harvest>(harvest =>
                harvest.Path == filePaths[0] &&
                harvest.HarvestType == Resources.RegionHarvesterExtensionFile &&
                harvest.Items.Count == 1 &&
                harvest.Items[0].Tag == "test1" &&
                harvest.Items[0].Content == "\ncontentpart1\r\n")), Times.Once);

            mockedCreateRepository.Verify(x => x.Create(It.Is<Harvest>(harvest =>
                harvest.Path == filePaths[1] &&
                harvest.HarvestType == Resources.RegionHarvesterExtensionFile &&
                harvest.Items.Count == 1 &&
                harvest.Items[0].Tag == "test2" &&
                harvest.Items[0].Content == "\n    contentpart2\r\n    ")), Times.Once);

            mockedCreateRepository.Verify(x => x.Create(It.Is<Harvest>(harvest =>
                harvest.Path == filePaths[2] &&
                harvest.HarvestType == Resources.RegionHarvesterExtensionFile &&
                harvest.Items.Count == 0)), Times.Once);
        }
    }
}
