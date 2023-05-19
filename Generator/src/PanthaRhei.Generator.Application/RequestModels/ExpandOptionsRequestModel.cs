using System;
using LiquidVisions.PanthaRhei.Generator.Domain;

namespace LiquidVisions.PanthaRhei.Generator.Application.RequestModels
{
    /// <summary>
    /// Request model with user options used during the expansion process.
    /// </summary>
    public class ExpandOptionsRequestModel
    {
        /// <summary>
        /// Gets or sets the AppId parameter.
        /// </summary>
        public virtual Guid AppId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the output needs to be cleaned.
        /// </summary>
        public virtual bool Clean { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the output needs to be cleaned.
        /// </summary>
        public virtual bool ReSeed { get; set; } = false;

        /// <summary>
        /// Gets or sets the name of the connectionstring.
        /// </summary>
        public virtual string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the RunMode value.
        /// </summary>
        public virtual string GenerationMode { get; set; }

        /// <summary>
        /// Gets or sets the root for the generator process.
        /// </summary>
        public virtual string Root { get; set; }

        /// <summary>
        /// Gets or sets the location of the folder where the Expanders are located.
        /// </summary>
        public virtual string ExpandersFolder { get; set; }
            = Resources.DefaultExpanderFolder;

        /// <summary>
        /// Gets or sets the location of the folder where the code harvestings are located.
        /// </summary>
        public virtual string HarvestFolder { get; set; }
            = Resources.DefaultHarvestFolder;

        /// <summary>
        /// Gets or sets the location where the generated code will be stored.
        /// </summary>
        public virtual string OutputFolder { get; set; }
            = Resources.DefaultOutputFolder;
    }
}
