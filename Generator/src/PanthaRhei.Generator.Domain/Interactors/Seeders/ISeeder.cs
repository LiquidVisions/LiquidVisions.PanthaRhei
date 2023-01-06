namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Seeders
{
    public interface ISeeder<in TEntity>
        where TEntity : class
    {
        int SeedOrder { get; }

        int ResetOrder { get; }

        void Reset();

        void Seed(TEntity entity);
    }
}
