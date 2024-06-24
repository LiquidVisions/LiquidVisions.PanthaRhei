using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LiquidVisions.PanthaRhei.Domain.Entities
{
    /// <summary>
    /// A model representing an application.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class App
    {
        /// <summary>
        /// Gets or sets the id of the <seealso cref="App"/>.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the <seealso cref="App"/>.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the full name of the <seealso cref="App"/>.
        /// </summary>
        public virtual string FullName { get; set; }

        /// <summary>
        /// Gets or sets a collection of <seealso cref="Expander" /> that are applicable for the <seealso cref="App"/>.
        /// </summary>
        public virtual ICollection<Expander> Expanders { get; set; } = [];

        /// <summary>
        /// Gets or sets a collection of <seealso cref="Entity" /> that are applicable for the <seealso cref="App"/>.
        /// </summary>
        public virtual ICollection<Entity> Entities { get; set; } = [];

        /// <summary>
        /// Gets or sets a collection of <seealso cref="ConnectionString" /> that are applicable for the <seealso cref="App"/>.
        /// </summary>
        public virtual ICollection<ConnectionString> ConnectionStrings { get; set; } = [];

        /// <summary>
        /// Gets or sets a collection of <seealso cref="Component"/>.
        /// </summary>
        public virtual ICollection<Component> Components { get; set; } = [];
    }
}
