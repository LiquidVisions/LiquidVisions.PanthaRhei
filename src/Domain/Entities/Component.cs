using System;
using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Domain.Entities
{
    /// <summary>
    /// Represents a <see cref="Component"/> entity.
    /// </summary>
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
        public virtual List<Package> Packages { get; set; } = new List<Package>();

        /// <summary>
        /// Gets or sets the <seealso cref="Expander">Expanders</seealso> of the <see cref="Component"/>.
        /// </summary>
        public virtual Expander Expander { get; set; }
    }
}
