using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LiquidVisions.PanthaRhei.Domain.Entities
{
    /// <summary>
    /// Represents a <see cref="Field"/> entity.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Field
    {
        /// <summary>
        /// Get or sets the Id of the <see cref="Field"/>.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the <see cref="Field"/>.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the ReturnType of the <see cref="Field"/>.
        /// </summary>
        public virtual string ReturnType { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating whether the <see cref="Field"/> is a collection.
        /// </summary>
        public virtual bool IsCollection { get; set; }

        /// <summary>
        /// Gets or sets the Modifier of the <see cref="Field"/>.
        /// </summary>
        public virtual string Modifier { get; set; }

        /// <summary>
        /// Gets or sets the GetModifier of the <see cref="Field"/>.
        /// </summary>
        public virtual string GetModifier { get; set; }

        /// <summary>
        /// Gets or sets the SetModifier of the <see cref="Field"/>.
        /// </summary>
        public virtual string SetModifier { get; set; }

        /// <summary>
        /// Gets or sets the Behaviour of the <see cref="Field"/>.
        /// </summary>
        public virtual string Behaviour { get; set; }

        /// <summary>
        /// Gets or sets the order of the <see cref="Field"/>.
        /// </summary>
        public virtual int Order { get; set; }

        /// <summary>
        /// Gets or sets the Size of the <see cref="Field"/>.
        /// </summary>
        public virtual int? Size { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating whether the <see cref="Field"/> is required.
        /// </summary>
        public virtual bool Required { get; set; }

        /// <summary>
        /// Gets or sets the <seealso cref="Entity"/> where the entity is referenced by
        /// </summary>
        public virtual Entity Reference { get; set; }

        /// <summary>
        /// Gets or sets the <seealso cref="Entity">Entity</seealso> the <see cref="Field"/> is a part of."/>
        /// </summary>
        public virtual Entity Entity { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating whether the <see cref="Field"/> is a key.
        /// </summary>
        public bool IsKey { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating whether the <see cref="Field"/> is an index.
        /// </summary>
        public bool IsIndex { get; set; }

        /// <summary>
        /// Gets or sets a list of <seealso cref="Relationship">Relationships</seealso> where the <see cref="Field"/> is a relationship."/>
        /// </summary>
        public virtual ICollection<Relationship> RelationshipKeys { get; set; }
            = new List<Relationship>();

        /// <summary>
        /// Gets or sets a list of <seealso cref="Relationship">Relationships</seealso> where the <see cref="Field"/> is a foreign key."/>
        /// </summary>
        public virtual ICollection<Relationship> IsForeignEntityKeyOf { get; set; }
            = new List<Relationship>();
    }
}
