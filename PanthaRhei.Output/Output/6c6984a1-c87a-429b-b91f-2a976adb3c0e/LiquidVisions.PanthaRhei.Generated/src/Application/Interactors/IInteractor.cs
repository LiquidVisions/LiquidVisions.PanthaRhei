using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors
{
    public interface IInteractor<TRequestModel>
        where TRequestModel : RequestModel, new()
    {
        Task<Response> ExecuteUseCase(TRequestModel model = null);
    }
}
