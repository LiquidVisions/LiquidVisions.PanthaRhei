using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Domain;

namespace LiquidVisions.PanthaRhei.Generated.Application.Gateways
{
    public interface ICreateGateway<TEntity> : IGateway<TEntity>
        where TEntity : class
    {
        Task<int> Create(TEntity entity);
    }
}
