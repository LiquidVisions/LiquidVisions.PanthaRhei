using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LiquidVisions.PanthaRhei.Domain.Entities
{
    /// <summary>
    /// Represents a <see cref="Component"/> entity.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Component
    {
        /// <summary>
        /// Gets or sets the Id of the <see cref="Component"/>.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the <see cref="Component"/>.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the Description of the <see cref="Component"/>.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the <seealso cref="Package">Packages</seealso> of the <see cref="Component"/>.
        /// </summary>
        public virtual ICollection<Package> Packages { get; set; } = [];

        /// <summary>
        /// Gets or sets the <seealso cref="Expander">Expanders</seealso> of the <see cref="Component"/>.
        /// </summary>
        public virtual Expander Expander { get; set; }
    }
}
