using System;
using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Models
{
    /// <summary>
    /// A model representing an application.
    /// </summary>
    public class App : IEntity
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
        /// Gets or sets the fullname of the <seealso cref="App"/>.
        /// </summary>
        public virtual string FullName { get; set; }

        /// <summary>
        /// Gets or sets a collection of <seealso cref="Expander" /> that are applicable for the <seealso cref="App"/>.
        /// </summary>
        public virtual IEnumerable<Expander> Expanders { get; set; }
    }
}
