namespace LiquidVisions.PanthaRhei.Domain.Repositories
{
    /// <summary>
    /// Represents a generic Delete Repository.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDeleteRepository<in TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Deletes a <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>A <seealso cref="bool"/> indicating wheather the entity has been deleted</returns>
        bool Delete(TEntity entity);

        /// <summary>
        /// Deletes all <typeparamref name="TEntity"/>.
        /// </summary>
        /// <returns>A <seealso cref="bool"/> indicating whether the all entities has been deleted.</returns>
        bool DeleteAll();

        /// <summary>
        /// Deletes a <typeparamref name="TEntity"/> by its Id.
        /// </summary>
        /// <param name="id">The Id of the entity</param>
        /// <returns>A <seealso cref="bool"/> indicating wether the entity has been deleted.</returns>
        bool DeleteById(object id);
    }
}
