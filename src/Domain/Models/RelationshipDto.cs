namespace LiquidVisions.PanthaRhei.Domain.Models
{
    /// <summary>
    /// Represents a <see cref="RelationshipDto"/> Data transferobject.
    /// </summary>
    public class RelationshipDto
    {
        /// <summary>
        /// Gets or sets the key of the <see cref="RelationshipDto"/>.
        /// </summary>
        public virtual string Key { get; set; }

        /// <summary>
        /// Gets or sets the Entity of the <see cref="RelationshipDto"/>.
        /// </summary>
        public virtual string Entity { get; set; }

        /// <summary>
        /// Gets or sets the Cardinality of the <see cref="RelationshipDto"/>.
        /// </summary>
        public virtual string Cardinality { get; set; }

        /// <summary>
        /// Gets or sets the Foreign Entity key of the <see cref="RelationshipDto"/>.
        /// </summary>
        public virtual string WithForeignEntityKey { get; set; }

        /// <summary>
        /// Gets or sets the Foreign Entity of the <see cref="RelationshipDto"/>.
        /// </summary>
        public virtual string WithForeignEntity { get; set; }

        /// <summary>
        /// Gets or sets the Cardinality of the <see cref="RelationshipDto"/>.
        /// </summary>
        public virtual string WithyCardinality { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating whether the <see cref="RelationshipDto"/> is required.
        /// </summary>
        public virtual bool Required { get; set; }
    }
}
