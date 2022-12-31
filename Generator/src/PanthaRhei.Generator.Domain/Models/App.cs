using System;

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
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the <seealso cref="App"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the fullname of the <seealso cref="App"/>.
        /// </summary>
        public string FullName { get; set; }
    }
}
