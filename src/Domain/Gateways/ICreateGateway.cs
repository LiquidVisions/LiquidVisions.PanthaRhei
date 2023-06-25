namespace LiquidVisions.PanthaRhei.Domain.Gateways
{
    public interface ICreateGateway<in TEntity>
        where TEntity : class
    {
        bool Create(TEntity entity);
    }
}
