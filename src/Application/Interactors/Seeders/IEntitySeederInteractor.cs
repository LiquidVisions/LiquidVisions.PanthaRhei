namespace LiquidVisions.PanthaRhei.Application.Interactors.Seeders
{
    public interface IEntitySeederInteractor<in TEntity>
        where TEntity : class
    {
        int SeedOrder { get; }

        int ResetOrder { get; }

        void Reset();

        void Seed(TEntity entity);
    }
}
