using System.Threading.Tasks;
using NS.Domain;

namespace NS.Application.Gateways
{
    public interface ICreateGateway<TEntity> : IGateway<TEntity>
        where TEntity : IEntity
    {
        Task<int> Create(TEntity entity);
    }
}
