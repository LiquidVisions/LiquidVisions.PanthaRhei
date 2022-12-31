using System.Diagnostics.CodeAnalysis;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Models
{
    public class GeneratorParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether the output needs to be cleaned.
        /// </summary>
        public virtual bool Clean { get; set; } = false;


        /// <summary>
        /// Gets or sets a value indicating the RunMode value.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public virtual GenerationModes GenerationMode { get; set; }
    }
}
