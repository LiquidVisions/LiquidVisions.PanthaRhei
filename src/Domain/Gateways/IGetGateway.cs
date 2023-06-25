using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Domain.Gateways
{
    public interface IGetGateway<out TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(object id);
    }
}
