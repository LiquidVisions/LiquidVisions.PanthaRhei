using System;
using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Domain.Entities
{
    /// <summary>
    /// Represents a model of an expander.
    /// </summary>
    public class Expander
    {
        /// <summary>
        /// Gets or sets the id of the <seealso cref="Expander"/>.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the <seealso cref="Expander"/>.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the expander is enabled.
        /// </summary>
        public virtual bool Enabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the order where the expander is executed in.
        /// </summary>
        public virtual int Order { get; set; }

        /// <summary>
        /// Gets or sets a collection of <seealso cref="App" /> that are applicable for the <seealso cref="Expander"/>.
        /// </summary>
        public virtual ICollection<App> Apps { get; set; }
            = new List<App>();

        /// <summary>
        /// Gets or sets a collecion of <seealso cref="Component">Handlers</seealso> that are applicable for the <seealso cref="Expander"/>.
        /// </summary>
        public virtual ICollection<Component> Components { get; set; }
            = new List<Component>();
    }
}
