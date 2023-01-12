using System;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Gateways
{
    public interface IUpdateGateway<in TEntity>
        where TEntity : class
    {
        bool Update(TEntity entity);
    }
}
