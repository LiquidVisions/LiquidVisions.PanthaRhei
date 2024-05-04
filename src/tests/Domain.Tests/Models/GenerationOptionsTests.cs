using System;
using System.IO;
using Xunit;

namespace LiquidVisions.PanthaRhei.Domain.Tests.Models
{
    /// <summary>
    /// Test for <see cref="GenerationOptions"/>.
    /// </summary>
    public class GenerationOptionsTests
    {
        private readonly GenerationOptions model;
        private readonly string root = @"C:\Root";

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerationOptionsTests"/> class.
        /// </summary>
        public GenerationOptionsTests()
        {
            model = new()
            {
                Root = root,
            };
        }

        /// <summary>
        /// Test for <see cref="GenerationOptions.ExpandersFolder"/>.
        /// </summary>
        [Fact]
        public void ExpandersFolderDefaultExpanderValueShouldBeEqual()
        {
            // arrange
            // act
            string result = model.ExpandersFolder;

            // assert
            Assert.Equal(result, Path.Combine(root, "Expanders"));
        }

        /// <summary>
        /// Test for <see cref="GenerationOptions.OutputFolder"/>.
        /// </summary>
        [Fact]
        public void OutputFolderDefaultValueShouldBeEqual()
        {
            // arrange
            model.AppId = Guid.NewGuid();

            // act
            string result = model.OutputFolder;

            // assert
            Assert.Equal(result, Path.Combine(root, "Output", model.AppId.ToString()));
        }

        /// <summary>
        /// Test for <see cref="GenerationOptions.HarvestFolder"/>.
        /// </summary>
        [Fact]
        public void HarvestFolderDefaultValueShouldBeEqual()
        {
            // arrange
            // act
            string result = model.HarvestFolder;

            // assert
            Assert.Equal(result, Path.Combine(root, "Harvest"));
        }

        /// <summary>
        /// Test for <see cref="GenerationOptions.ExpandersFolder"/>.
        /// </summary>
        [Fact]
        public void ExpandersFolderCustomValueShouldBeEqual()
        {
            // arrange
            string customvalue = "Custom";
            model.ExpandersFolder = customvalue;

            // act
            string result = model.ExpandersFolder;

            // assert
            Assert.Equal(result, Path.Combine(root, customvalue));
        }

        /// <summary>
        /// Test for <see cref="GenerationOptions.OutputFolder"/>.
        /// </summary>
        [Fact]
        public void OutputFolderCustomValueShouldBeEqual()
        {
            // arrange
            string customvalue = "Custom";
            model.OutputFolder = customvalue;
            model.AppId = Guid.NewGuid();

            // act
            string result = model.OutputFolder;

            // assert
            Assert.Equal(result, Path.Combine(root, customvalue, model.AppId.ToString()));
        }

        /// <summary>
        /// Test for <see cref="GenerationOptions.HarvestFolder"/>.
        /// </summary>
        [Fact]
        public void HarvestFolderCustomValueShouldBeEqual()
        {
            // arrange
            string customvalue = "Custom";
            model.HarvestFolder = customvalue;

            // act
            string result = model.HarvestFolder;

            // assert
            Assert.Equal(result, Path.Combine(root, customvalue));
        }
    }
}
