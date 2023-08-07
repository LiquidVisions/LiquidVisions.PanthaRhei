using System;
using NS.Domain;

namespace NS.Application.Gateways
{
    public interface IGetByIdGateway<TEntity> : IGateway<TEntity>
        where TEntity : class
    {
        TEntity GetById(Guid id);
    }
}
