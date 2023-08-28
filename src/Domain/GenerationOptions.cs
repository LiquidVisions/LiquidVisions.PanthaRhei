using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
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
        private string _harvestFolder = Resources.DefaultHarvestFolder;
        private string _expanderFolder = Resources.DefaultExpanderFolder;
        private string _outputFolder = Resources.DefaultOutputFolder;

        /// <summary>
        /// Gets or sets the AppId parameter.
        /// </summary>
        public virtual Guid AppId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the output needs to be cleaned.
        /// </summary>
        public virtual bool Clean { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the database schema should be attempted to update.
        /// </summary>
        public virtual bool Migrate { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the database should be seeded with the data of the meta model.
        /// </summary>
        public virtual bool Seed { get; set; } = false;

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
            get => Path.Combine(Root, _expanderFolder);
            set => _expanderFolder = value;
        }

        /// <summary>
        /// Gets or sets the location of the folder where the code harvestings are located.
        /// </summary>
        public virtual string HarvestFolder
        {
            get => Path.Combine(Root, _harvestFolder);
            set => _harvestFolder = value;
        }

        /// <summary>
        /// Gets or sets the location where the generated code will be stored.
        /// </summary>
        public virtual string OutputFolder
        {
            get => Path.Combine(Root, _outputFolder, AppId.ToString());
            set => _outputFolder = value;
        }

        /// <summary>
        /// prints all the properties of the <see cref="GenerationOptions"/> to a string.
        /// </summary>
        /// <returns>All public properties and values, as a <seealso cref="string"/></returns>
        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append("CommandParameters")
                .AppendLine(" { ");

            PropertyInfo[] list = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in list)
            {
                sb.AppendLine($" \"{property.Name}\": \"{property.GetValue(this)}\", ");
            }

            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
