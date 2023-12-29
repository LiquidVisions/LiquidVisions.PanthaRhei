using System;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using LiquidVisions.PanthaRhei.Domain;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.RequestModels
{
    /// <summary>
    /// Tests for <seealso cref="ExpandOptionsRequestModel"/>.
    /// </summary>
    public class ExpandOptionsRequestModelTests
    {
        /// <summary>
        /// Tests all properties.
        /// </summary>
        [Fact]
        public void TestAllProperties()
        {
            // Arrange
            var model = new ExpandOptionsRequestModel();
            var appId = Guid.NewGuid();
            bool migrate = true;
            bool clean = true;
            bool seed = true;
            string connectionString = "test connection string";
            string generationMode = "test generation mode";
            string root = "test root";
            string expandersFolder = "test expanders folder";
            string harvestFolder = "test harvest folder";
            string outputFolder = "test output folder";

            // Act
            model.AppId = appId;
            model.Migrate = migrate;
            model.Clean = clean;
            model.Seed = seed;
            model.ConnectionString = connectionString;
            model.GenerationMode = generationMode;
            model.Root = root;
            model.ExpandersFolder = expandersFolder;
            model.HarvestFolder = harvestFolder;
            model.OutputFolder = outputFolder;

            // Assert
            Assert.Equal(appId, model.AppId);
            Assert.Equal(migrate, model.Migrate);
            Assert.Equal(clean, model.Clean);
            Assert.Equal(seed, model.Seed);
            Assert.Equal(connectionString, model.ConnectionString);
            Assert.Equal(generationMode, model.GenerationMode);
            Assert.Equal(root, model.Root);
            Assert.Equal(expandersFolder, model.ExpandersFolder);
            Assert.Equal(harvestFolder, model.HarvestFolder);
            Assert.Equal(outputFolder, model.OutputFolder);
        }

        /// <summary>
        /// tests the default values.
        /// </summary>
        [Fact]
        public void TestDefaultValues()
        {
            // Arrange
            var model = new ExpandOptionsRequestModel();

            // Assert
            Assert.Equal(Resources.DefaultExpanderFolder, model.ExpandersFolder);
            Assert.Equal(Resources.DefaultHarvestFolder, model.HarvestFolder);
            Assert.Equal(Resources.DefaultOutputFolder, model.OutputFolder);
        }

        
    }
}
