using System;
using System.Diagnostics.CodeAnalysis;

namespace LiquidVisions.PanthaRhei.Domain.Entities
{
    /// <summary>
    /// Represents a <see cref="Relationship"/> entity.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Relationship
    {
        /// <summary>
        /// Gets or sets the Id of the <see cref="Relationship"/>.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the <seealso cref="Field">Key</seealso> of the <see cref="Relationship"/>.
        /// </summary>
        public virtual Field Key { get; set; }

        /// <summary>
        /// Gets or sets the <seealso cref="Entity">Entity</seealso> of the <see cref="Relationship"/>. 
        /// </summary>
        public virtual Entity Entity { get; set; }

        /// <summary>
        /// Gets or sets the Cardinality of the <see cref="Relationship"/>.
        /// </summary>
        public virtual string Cardinality { get; set; }

        /// <summary>
        /// Gets or sets the <seealso cref="Field">Field of the Foreign Entity key</seealso> of the <see cref="Relationship"/>.
        /// </summary>
        public virtual Field WithForeignEntityKey { get; set; }

        /// <summary>
        /// Gets or sets the <seealso cref="Entity">Field of the Foreign Entity</seealso> of the <see cref="Relationship"/>.
        /// </summary>
        public virtual Entity WithForeignEntity { get; set; }

        /// <summary>
        /// Gets or sets the Cardinality of the <see cref="Relationship"/>.
        /// </summary>
        public virtual string WithCardinality { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating whether the <see cref="Relationship"/> is required.   
        /// </summary>
        public virtual bool Required { get; set; }
    }
}
