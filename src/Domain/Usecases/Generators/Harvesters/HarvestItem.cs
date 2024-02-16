using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Serialization;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters
{
    /// <summary>
    /// Represents the model for an instance with detailed <see cref="Harvest"/> information from a harvested file.
    /// </summary>
    public class HarvestItem
    {
        /// <summary>
        /// Gets or sets the content of the object that needs to be harvested.
        /// </summary>
        [XmlIgnore]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the name of the tag that is used in congestion with the content.
        /// </summary>
        [XmlIgnore]
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the content as an XML CData block, deeded for serialisation.
        /// </summary>
        [XmlElement(nameof(Content))]
        public XmlCDataSection ContentXml
        {
            get { return new XmlDocument().CreateCDataSection(Content); }
            set
            {
                ArgumentNullException.ThrowIfNull(value);
                Content = value.Value;
            }
        }

        /// <summary>
        /// Gets or sets the tag as an XML CData block, deeded for serialisation.
        /// </summary>
        public XmlCDataSection TagXml
        {
            get { return new XmlDocument().CreateCDataSection(Tag); }
            set
            {
                ArgumentNullException.ThrowIfNull(value);
                Tag = value.Value;
            }
        }
    }
}
