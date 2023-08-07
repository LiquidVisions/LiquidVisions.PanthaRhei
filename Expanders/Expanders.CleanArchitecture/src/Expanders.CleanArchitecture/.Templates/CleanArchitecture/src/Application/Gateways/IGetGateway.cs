using System.Linq;
using NS.Domain;

namespace NS.Application.Gateways
{
    public interface IGetGateway<TEntity> : IGateway<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Get();
    }
}
