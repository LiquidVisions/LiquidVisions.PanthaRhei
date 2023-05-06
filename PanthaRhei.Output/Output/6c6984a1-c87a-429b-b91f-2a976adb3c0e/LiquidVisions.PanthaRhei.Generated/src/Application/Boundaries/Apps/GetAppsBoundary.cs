using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Apps
{
    internal class GetAppsBoundary : IBoundary<GetAppsRequestModel>
    {
        private readonly IInteractor<GetAppsRequestModel> interactor;

        public GetAppsBoundary(IInteractor<GetAppsRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetAppsRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
