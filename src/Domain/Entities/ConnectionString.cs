using System;

namespace LiquidVisions.PanthaRhei.Domain.Entities
{
    /// <summary>
    /// Represents a <see cref="ConnectionString"/> entity.
    /// </summary>
    public class ConnectionString
    {
        /// <summary>
        /// Gets or sets the Id of the <see cref="ConnectionString"/>.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the <see cref="ConnectionString"/>.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the Definition of the <see cref="ConnectionString"/>.
        /// </summary>
        public virtual string Definition { get; set; }

        /// <summary>
        /// Gets or sets the <seealso cref="App">App</seealso> the <see cref="ConnectionString"/> is a part of.
        /// </summary>
        public virtual App App { get; set; }
    }
}
