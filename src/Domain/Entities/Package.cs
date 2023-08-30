using System;

namespace LiquidVisions.PanthaRhei.Domain.Entities
{
    /// <summary>
    /// Represents a <see cref="Package"/> entity.
    /// </summary>
    public class Package
    {
        /// <summary>
        /// Gets or sets the Id of the <see cref="Package"/>.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the <see cref="Package"/>.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the Version of the <see cref="Package"/>.
        /// </summary>
        public virtual string Version { get; set; }

        /// <summary>
        /// Gets or sets the <seealso cref="Component"/> of the <see cref="Package"/>.
        /// </summary>
        public virtual Component Component { get; set; }
    }
}
