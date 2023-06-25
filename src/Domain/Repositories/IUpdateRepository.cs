using System;

namespace LiquidVisions.PanthaRhei.Domain.Repositories
{
    public interface IUpdateRepository<in TEntity>
        where TEntity : class
    {
        bool Update(TEntity entity);
    }
}
