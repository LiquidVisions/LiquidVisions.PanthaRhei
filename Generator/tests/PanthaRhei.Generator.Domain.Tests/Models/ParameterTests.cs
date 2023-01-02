using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Tests.Models
{
    public class ParameterTests
    {
        private readonly Parameters model;
        private readonly string root = @"C:\Root";

        public ParameterTests()
        {
            model = new()
            {
                Root = root,
            };
        }

        [Fact]
        public void ExpandersFolder_DefaultExpanderValue_ShouldBeEqual()
        {
            // arrange
            // act
            string result = model.ExpandersFolder;

            // assert
            Assert.Equal(result, Path.Combine(root, "Expanders"));
        }

        [Fact]
        public void OutputFolder_DefaultValue_ShouldBeEqual()
        {
            // arrange
            // act
            string result = model.OutputFolder;

            // assert
            Assert.Equal(result, Path.Combine(root, "Output"));
        }

        [Fact]
        public void HarvestFolder_DefaultValue_ShouldBeEqual()
        {
            // arrange
            // act
            string result = model.HarvestFolder;

            // assert
            Assert.Equal(result, Path.Combine(root, "Harvest"));
        }

        [Fact]
        public void ExpandersFolder_CustomValue_ShouldBeEqual()
        {
            // arrange
            string customvalue = "Custom";
            model.ExpandersFolder = customvalue;

            // act
            string result = model.ExpandersFolder;

            // assert
            Assert.Equal(result, Path.Combine(root, customvalue));
        }

        [Fact]
        public void OutputFolder_CustomValue_ShouldBeEqual()
        {
            // arrange
            string customvalue = "Custom";
            model.OutputFolder = customvalue;

            // act
            string result = model.OutputFolder;

            // assert
            Assert.Equal(result, Path.Combine(root, customvalue));
        }

        [Fact]
        public void HarvestFolder_CustomValue_ShouldBeEqual()
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
