using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Apps
{
    internal class GetAppsBoundary : IBoundary<GetAppsQuery>
    {
        private readonly IInteractor<GetAppsQuery> interactor;

        public GetAppsBoundary(IInteractor<GetAppsQuery> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetAppsQuery requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
