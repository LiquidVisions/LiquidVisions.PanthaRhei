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
                Content = "content_test",
                Tag = "tag_test"
            };

            // act
            XmlCDataSection contentSection = item.ContentXml;
            XmlCDataSection tagSection = item.TagXml;

            // assert
            Assert.Equal("content_test", contentSection.Value);
            Assert.Equal("tag_test", tagSection.Value);
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
                ContentXml = new XmlDocument().CreateCDataSection("content_test"),
                TagXml = new XmlDocument().CreateCDataSection("tag_test")
            };

            // act
            string content = item.Content;
            string tag = item.Tag;

            // assert
            Assert.Equal("content_test", content);
            Assert.Equal("tag_test", tag);
        }
    }
}
