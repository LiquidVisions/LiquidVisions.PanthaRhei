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
        private readonly GenerationOptions _model;
        private readonly string _root = @"C:\Root";

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerationOptionsTests"/> class.
        /// </summary>
        public GenerationOptionsTests()
        {
            _model = new()
            {
                Root = _root,
            };
        }

        /// <summary>
        /// Test for <see cref="GenerationOptions.ExpandersFolder"/>.
        /// </summary>
        [Fact]
        public void ExpandersFolder_DefaultExpanderValue_ShouldBeEqual()
        {
            // arrange
            // act
            string result = _model.ExpandersFolder;

            // assert
            Assert.Equal(result, Path.Combine(_root, "Expanders"));
        }

        /// <summary>
        /// Test for <see cref="GenerationOptions.OutputFolder"/>.
        /// </summary>
        [Fact]
        public void OutputFolder_DefaultValue_ShouldBeEqual()
        {
            // arrange
            _model.AppId = Guid.NewGuid();

            // act
            string result = _model.OutputFolder;

            // assert
            Assert.Equal(result, Path.Combine(_root, "Output", _model.AppId.ToString()));
        }

        /// <summary>
        /// Test for <see cref="GenerationOptions.HarvestFolder"/>.
        /// </summary>
        [Fact]
        public void HarvestFolder_DefaultValue_ShouldBeEqual()
        {
            // arrange
            // act
            string result = _model.HarvestFolder;

            // assert
            Assert.Equal(result, Path.Combine(_root, "Harvest"));
        }

        /// <summary>
        /// Test for <see cref="GenerationOptions.ExpandersFolder"/>.
        /// </summary>
        [Fact]
        public void ExpandersFolder_CustomValue_ShouldBeEqual()
        {
            // arrange
            string customvalue = "Custom";
            _model.ExpandersFolder = customvalue;

            // act
            string result = _model.ExpandersFolder;

            // assert
            Assert.Equal(result, Path.Combine(_root, customvalue));
        }

        /// <summary>
        /// Test for <see cref="GenerationOptions.OutputFolder"/>.
        /// </summary>
        [Fact]
        public void OutputFolder_CustomValue_ShouldBeEqual()
        {
            // arrange
            string customvalue = "Custom";
            _model.OutputFolder = customvalue;
            _model.AppId = Guid.NewGuid();

            // act
            string result = _model.OutputFolder;

            // assert
            Assert.Equal(result, Path.Combine(_root, customvalue, _model.AppId.ToString()));
        }

        /// <summary>
        /// Test for <see cref="GenerationOptions.HarvestFolder"/>.
        /// </summary>
        [Fact]
        public void HarvestFolder_CustomValue_ShouldBeEqual()
        {
            // arrange
            string customvalue = "Custom";
            _model.HarvestFolder = customvalue;

            // act
            string result = _model.HarvestFolder;

            // assert
            Assert.Equal(result, Path.Combine(_root, customvalue));
        }
    }
}
