using System;
using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Models
{
    /// <summary>
    /// Represents a model of an expander.
    /// </summary>
    public class Expander : IEntity
    {
        /// <summary>
        /// Gets or sets the id of the <seealso cref="Expander"/>.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the <seealso cref="Expander"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a collection of <seealso cref="App" /> that are applicable for the <seealso cref="Expander"/>.
        /// </summary>
        public virtual List<App> Apps { get; set; }
    }
}
