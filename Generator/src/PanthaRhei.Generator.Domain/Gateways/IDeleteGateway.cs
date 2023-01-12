namespace LiquidVisions.PanthaRhei.Generator.Domain.Gateways
{
    public interface IDeleteGateway<in TEntity>
        where TEntity : class
    {
        bool Delete(TEntity entity);

        bool DeleteAll();

        bool DeleteById(object id);
    }
}
