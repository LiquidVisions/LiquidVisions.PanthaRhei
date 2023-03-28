namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders
{
    public interface ISeederInteractor<in TEntity>
        where TEntity : class
    {
        int SeedOrder { get; }

        int ResetOrder { get; }

        void Reset();

        void Seed(TEntity entity);
    }
}
