using System;
using System.Xml;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Domain.Tests.UseCases.Generators
{
    /// <summary>
    /// Tests for <seealso cref="HarvestItem"/>
    /// </summary>
    public class HarvestItemTests
    {
        /// <summary>
        /// Tests the Xml Properties for proper values.
        /// </summary>
        [Fact]
        public void ProperTestName()
        {
            // arrange
            HarvestItem item = new()
            {
                Content = "contenttest",
                Tag = "tagtest"
            };

            // act
            XmlCDataSection contentSection = item.ContentXml;
            XmlCDataSection tagSection = item.TagXml;

            // assert
            Assert.Equal("contenttest", contentSection.Value);
            Assert.Equal("tagtest", tagSection.Value);
        }

        /// <summary>
        /// Tests Xml properties for null values.
        /// </summary>
        [Fact]
        public void XmlPropertiesShouldNotAllowNullArguments()
        {
            // arrange
            HarvestItem item = new();

            // act
            // assert
            Assert.Throws<ArgumentNullException>(() => item.ContentXml = null);
            Assert.Throws<ArgumentNullException>(() => item.TagXml = null);
        }

        /// <summary>
        /// Tests conversion to CData tags to proper values in Xml properties.
        /// </summary>
        [Fact]
        public void TestsConversionToCDataTagsToProperValuesInNonXmlProperties()
        {
            // arrange
            HarvestItem item = new()
            {
                ContentXml = new XmlDocument().CreateCDataSection("contenttest"),
                TagXml = new XmlDocument().CreateCDataSection("tagtest")
            };

            // act
            string content = item.Content;
            string tag = item.Tag;

            // assert
            Assert.Equal("contenttest", content);
            Assert.Equal("tagtest", tag);
        }
    }
}
