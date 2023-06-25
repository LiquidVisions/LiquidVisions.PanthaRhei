namespace LiquidVisions.PanthaRhei.Application.Usecases.Seeders
{
    public interface IEntitySeeder<in TEntity>
        where TEntity : class
    {
        int SeedOrder { get; }

        int ResetOrder { get; }

        void Reset();

        void Seed(TEntity entity);
    }
}
