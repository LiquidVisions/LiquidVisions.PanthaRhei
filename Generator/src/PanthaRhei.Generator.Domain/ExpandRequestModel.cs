using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain
{
    /// <summary>
    /// Global requestmodel for the generation operations.
    /// </summary>
    public class ExpandRequestModel
    {
        private string harvestFolder = "Harvest";
        private string expanderFolder = "Expanders";
        private string outputFolder = "Output";

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

        public virtual string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the RunMode value.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public virtual GenerationModes GenerationMode { get; set; }

        /// <summary>
        /// Gets or sets the root for the generator process.
        /// </summary>
        public virtual string Root { get; set; }

        /// <summary>
        /// Gets or sets the location of the folder where the <seealso cref="IExpanderInteractor">Expanders</seealso> are located.
        /// </summary>
        public virtual string ExpandersFolder
        {
            get => Path.Combine(Root, expanderFolder);
            set => expanderFolder = value;
        }

        /// <summary>
        /// Gets or sets the location of the folder where the code harvestings are located.
        /// </summary>
        public virtual string HarvestFolder
        {
            get => Path.Combine(Root, harvestFolder);
            set => harvestFolder = value;
        }

        /// <summary>
        /// Gets or sets the location where the generated code will be stored.
        /// </summary>
        public virtual string OutputFolder
        {
            get => Path.Combine(Root, outputFolder, AppId.ToString());
            set => outputFolder = value;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append("CommandParameters")
                .AppendLine(" { ");

            this.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToList()
                .ForEach(property => sb.AppendLine($" \"{property.Name}\": \"{property.GetValue(this)}\", "));

            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
