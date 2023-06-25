namespace LiquidVisions.PanthaRhei.Domain.Repositories
{
    public interface IDeleteRepository<in TEntity>
        where TEntity : class
    {
        bool Delete(TEntity entity);

        bool DeleteAll();

        bool DeleteById(object id);
    }
}
