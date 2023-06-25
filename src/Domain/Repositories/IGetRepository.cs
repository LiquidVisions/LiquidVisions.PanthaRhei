using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Domain.Repositories
{
    public interface IGetRepository<out TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(object id);
    }
}
