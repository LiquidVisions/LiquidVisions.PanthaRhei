using System.Threading.Tasks;
using NS.Domain;

namespace NS.Application.Gateways
{
    public interface IUpdateGateway<TEntity> : IGateway<TEntity>
        where TEntity : class
    {
        Task<int> Update(TEntity entity);
    }
}
