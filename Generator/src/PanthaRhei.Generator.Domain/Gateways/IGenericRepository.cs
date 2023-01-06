using System;
using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Gateways
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        Type ContextType { get; }

        IEnumerable<TEntity> GetAll();

        TEntity GetById(object id);

        bool Create(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(TEntity entity);

        bool DeleteAll();

        bool DeleteById(object id);
    }
}
