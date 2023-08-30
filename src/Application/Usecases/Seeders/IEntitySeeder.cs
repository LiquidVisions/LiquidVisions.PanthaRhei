namespace LiquidVisions.PanthaRhei.Application.Usecases.Seeders
{
    /// <summary>
    /// provides a way to seed an entity.
    /// </summary>
    /// <typeparam name="TEntity">A generic parameter representing an entity.</typeparam>
    public interface IEntitySeeder<in TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets the order in which the seeder should be executed.
        /// </summary>
        int SeedOrder { get; }

        /// <summary>
        /// Gets the order in which the seeder should be reset.
        /// </summary>
        int ResetOrder { get; }

        /// <summary>
        /// Resets the seeder.
        /// </summary>
        void Reset();

        /// <summary>
        /// Seeds the entity.
        /// </summary>
        /// <param name="entity">The entity</param>
        void Seed(TEntity entity);
    }
}
