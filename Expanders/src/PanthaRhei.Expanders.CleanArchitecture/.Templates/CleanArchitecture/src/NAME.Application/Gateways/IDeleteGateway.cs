using System.Threading.Tasks;
using NS.Domain;

namespace NS.Application.Gateways
{
    public interface IDeleteGateway<TEntity> : IGateway<TEntity>
        where TEntity : IEntity
    {
        Task<bool> Delete(TEntity entity);
    }
}
