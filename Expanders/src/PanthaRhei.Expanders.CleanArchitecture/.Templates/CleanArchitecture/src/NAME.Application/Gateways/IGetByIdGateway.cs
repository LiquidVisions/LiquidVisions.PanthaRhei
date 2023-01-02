using System;
using NS.Domain;

namespace NS.Application.Gateways
{
    public interface IGetByIdGateway<TEntity> : IGateway<TEntity>
        where TEntity : IEntity
    {
        TEntity GetById(Guid id);
    }
}
