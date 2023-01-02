using System.Linq;
using NS.Domain;

namespace NS.Application.Gateways
{
    public interface IGetGateway<TEntity> : IGateway<TEntity>
        where TEntity : IEntity
    {
        IQueryable<TEntity> Get();
    }
}
