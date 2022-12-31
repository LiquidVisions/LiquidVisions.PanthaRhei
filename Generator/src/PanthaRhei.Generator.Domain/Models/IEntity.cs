using System;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Models
{
    /// <summary>
    /// Specifies a contract for an entity object.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the Id of the <see cref="IEntity"/>.
        /// </summary>
        public Guid Id { get; set; }
    }
}
