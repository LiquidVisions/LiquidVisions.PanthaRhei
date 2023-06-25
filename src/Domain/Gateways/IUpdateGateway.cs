using System;

namespace LiquidVisions.PanthaRhei.Domain.Gateways
{
    public interface IUpdateGateway<in TEntity>
        where TEntity : class
    {
        bool Update(TEntity entity);
    }
}
