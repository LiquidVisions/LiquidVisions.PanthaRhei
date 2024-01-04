using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LiquidVisions.PanthaRhei.Domain.Entities
{
    /// <summary>
    /// Represents a <see cref="Entity"/> entity.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Entity
    {
        /// <summary>
        /// Gets or sets the Id of the <see cref="Entity"/>.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the <see cref="Entity"/>.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the location of the <see cref="Entity"/>.
        /// </summary>
        public virtual string Callsite { get; set; }

        /// <summary>
        /// Gets or sets the Type of the <see cref="Entity"/>.
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// Gets or sets the Modifier of the <see cref="Entity"/>.
        /// </summary>
        public virtual string Modifier { get; set; }

        /// <summary>
        /// Gets or sets the Behaviour of the <see cref="Entity"/>.
        /// </summary>
        public virtual string Behaviour { get; set; }

        /// <summary>
        /// Gets or sets the <seealso cref="App">App</seealso> the <see cref="Entity"/> is a part of.
        /// </summary>
        public virtual App App { get; set; }

        /// <summary>
        /// Gets or sets the <seealso cref="Field">Fields</seealso> of the <see cref="Entity"/>.
        /// </summary>
        public virtual ICollection<Field> Fields { get; set; }
            = new List<Field>();

        /// <summary>
        /// Gets or sets the <seealso cref="Field">Fields</seealso> of the <see cref="Entity"/> that are referenced by other <seealso cref="Entity">Entities</seealso>.
        /// </summary>
        public virtual ICollection<Field> ReferencedIn { get; set; }
            = new List<Field>();

        /// <summary>
        /// Gets or sets the <seealso cref="Relationship">Relationships</seealso> of the <see cref="Entity"/>.
        /// </summary>
        public virtual ICollection<Relationship> Relations { get; set; }
            = new List<Relationship>();

        /// <summary>
        /// Gets or sets the <seealso cref="Relationship">Relationships</seealso> of the <see cref="Entity"/> that are referenced by other <seealso cref="Entity">Entities</seealso>.
        /// </summary>
        public virtual ICollection<Relationship> IsForeignEntityOf { get; set; }
            = new List<Relationship>();
    }
}
