﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Domain
{
    /// <summary>
    /// Global requestmodel for the generation operations.
    /// </summary>
    public class GenerationOptions
    {
        private string harvestFolder = Resources.DefaultHarvestFolder;
        private string expanderFolder = Resources.DefaultExpanderFolder;
        private string outputFolder = Resources.DefaultOutputFolder;

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
        [ExcludeFromCodeCoverage]
        public virtual GenerationModes Modes { get; set; }

        /// <summary>
        /// Gets or sets the root for the generator process.
        /// </summary>
        public virtual string Root { get; set; }

        /// <summary>
        /// Gets or sets the location of the folder where the <seealso cref="IExpander">Expanders</seealso> are located.
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

            var list = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach(var property in list)
            {
                sb.AppendLine($" \"{property.Name}\": \"{property.GetValue(this)}\", ");
            }

            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
