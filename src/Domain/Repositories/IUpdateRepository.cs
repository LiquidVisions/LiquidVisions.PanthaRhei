using System;

namespace LiquidVisions.PanthaRhei.Domain.Repositories
{
    /// <summary>
    /// Represents an interface for a generic Update Repository.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IUpdateRepository<in TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Updates a <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="entity">The Entity</param>
        /// <returns>A boolean indicating whether the update has succeeded.</returns>
        bool Update(TEntity entity);
    }
}
