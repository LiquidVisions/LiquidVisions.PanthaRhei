using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain
{
    /// <summary>
    /// Global parameters for the generation operations.
    /// </summary>
    public class Parameters
    {
        private string harvestFolder = "Harvest";
        private string expanderFolder = "Expanders";
        private string outputFolder = "Output";

        /// <summary>
        /// Gets or sets the AppId parameter.
        /// </summary>
        public Guid AppId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the output needs to be cleaned.
        /// </summary>
        public virtual bool Clean { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the output needs to be cleaned.
        /// </summary>
        public virtual bool ReSeed { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating the RunMode value.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public virtual GenerationModes GenerationMode { get; set; }

        /// <summary>
        /// Gets or sets the root for the generator process.
        /// </summary>
        public string Root { get; set; }

        /// <summary>
        /// Gets or sets the location of the folder where the <seealso cref="IExpander">Expanders</seealso> are located.
        /// </summary>
        public string ExpandersFolder
        {
            get => Path.Combine(Root, expanderFolder);
            set => expanderFolder = value;
        }

        /// <summary>
        /// Gets or sets the location of the folder where the code harvestings are located.
        /// </summary>
        public string HarvestFolder
        {
            get => Path.Combine(Root, harvestFolder);
            set => harvestFolder = value;
        }

        /// <summary>
        /// Gets or sets the location where the generated code will be stored.
        /// </summary>
        public string OutputFolder
        {
            get => Path.Combine(Root, outputFolder, AppId.ToString());
            set => outputFolder = value;
        }
    }
}
