using System;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Models
{
    public class Handler
    {
        /// <summary>
        /// Gets or sets the Id of the <seealso cref="Handler"/>.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the <seealso cref="Handler"/>.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the Order of the <seealso cref="Handler"/>.
        /// </summary>
        public virtual int Order { get; set; }

        /// <summary>
        /// Gets or sets <seealso cref="Expander"/> of the <seealso cref="Handler"/>.
        /// </summary>
        public virtual Expander Expander { get; set; }

        /// <summary>
        /// Gets or sets the <seealso cref="GenerationModes">Supported generation modes</seealso> of the <seealso cref="Handler"/>.
        /// </summary>
        public virtual GenerationModes SupportedGenerationModes { get; set; } = GenerationModes.Default;
    }
}
