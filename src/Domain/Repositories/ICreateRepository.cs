namespace LiquidVisions.PanthaRhei.Domain.Repositories
{
    /// <summary>
    /// Represents a generic Create Repository.
    /// </summary>
    /// <typeparam name="TEntity">The entity</typeparam>
    public interface ICreateRepository<in TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Creates a new <typeparamref name="TEntity"/>.   
        /// </summary>
        /// <param name="entity">The Entity</param>
        /// <returns>An persisted version of the entity.</returns>
        bool Create(TEntity entity);
    }
}
