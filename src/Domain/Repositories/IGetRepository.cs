using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Domain.Repositories
{
    /// <summary>
    /// Represents a generic Get Repository.
    /// </summary>
    /// <typeparam name="TEntity">A generic type representing an entity.</typeparam>
    public interface IGetRepository<out TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets all <typeparamref name="TEntity">Entities</typeparamref>.
        /// </summary>
        /// <returns>A <seealso cref="IEnumerable{T}">List of entities</seealso>.</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Gets a <typeparamref name="TEntity"/> by its Id.
        /// </summary>
        /// <param name="id">The Id of the Entity.</param>
        /// <returns>The entity</returns>
        TEntity GetById(object id);
    }
}
