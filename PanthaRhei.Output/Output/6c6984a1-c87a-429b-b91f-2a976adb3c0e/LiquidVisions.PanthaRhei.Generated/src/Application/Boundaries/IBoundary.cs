using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries
{
    public interface IBoundary<TRequestModel>
        where TRequestModel : RequestModel, new()
    {
        Task Execute(TRequestModel requestModel, IPresenter presenter);
    }
}
