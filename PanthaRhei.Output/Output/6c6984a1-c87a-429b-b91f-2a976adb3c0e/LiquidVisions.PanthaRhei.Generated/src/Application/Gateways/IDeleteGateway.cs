using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Domain;

namespace LiquidVisions.PanthaRhei.Generated.Application.Gateways
{
    public interface IDeleteGateway<TEntity> : IGateway<TEntity>
        where TEntity : class
    {
        Task<bool> Delete(TEntity entity);
    }
}
