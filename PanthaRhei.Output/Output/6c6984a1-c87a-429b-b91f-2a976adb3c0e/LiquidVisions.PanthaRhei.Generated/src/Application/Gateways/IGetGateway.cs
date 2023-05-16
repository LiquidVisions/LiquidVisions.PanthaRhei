using System.Linq;
using LiquidVisions.PanthaRhei.Generated.Domain;

namespace LiquidVisions.PanthaRhei.Generated.Application.Gateways
{
    public interface IGetGateway<TEntity> : IGateway<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Get();
    }
}
