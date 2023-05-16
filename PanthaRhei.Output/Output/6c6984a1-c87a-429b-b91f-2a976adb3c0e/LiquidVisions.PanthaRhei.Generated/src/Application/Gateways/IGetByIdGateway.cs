using System;
using LiquidVisions.PanthaRhei.Generated.Domain;

namespace LiquidVisions.PanthaRhei.Generated.Application.Gateways
{
    public interface IGetByIdGateway<TEntity> : IGateway<TEntity>
        where TEntity : class
    {
        TEntity GetById(Guid id);
    }
}
