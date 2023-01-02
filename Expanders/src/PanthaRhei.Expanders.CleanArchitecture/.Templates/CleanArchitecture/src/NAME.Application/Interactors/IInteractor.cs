using System.Threading.Tasks;
using NS.Client.RequestModels;

namespace NS.Application.Interactors
{
    public interface IInteractor<TRequestModel>
        where TRequestModel : RequestModel, new()
    {
        Task<Response> ExecuteUseCase(TRequestModel model = null);
    }
}
