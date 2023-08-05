using System.Threading.Tasks;
using NS.Domain;

namespace NS.Application.Gateways
{
    public interface IDeleteGateway<TEntity> : IGateway<TEntity>
        where TEntity : class
    {
        Task<bool> Delete(TEntity entity);
    }
}
