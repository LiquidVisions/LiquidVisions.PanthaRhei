namespace LiquidVisions.PanthaRhei.Domain.Repositories
{
    public interface ICreateRepository<in TEntity>
        where TEntity : class
    {
        bool Create(TEntity entity);
    }
}
