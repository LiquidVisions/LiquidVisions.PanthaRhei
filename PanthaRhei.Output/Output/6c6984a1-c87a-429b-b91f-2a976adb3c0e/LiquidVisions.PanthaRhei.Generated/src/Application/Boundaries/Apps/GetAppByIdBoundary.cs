using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Apps
{
    internal class GetAppByIdBoundary : IBoundary<GetAppByIdQuery>
    {
        private readonly IInteractor<GetAppByIdQuery> interactor;

        public GetAppByIdBoundary(IInteractor<GetAppByIdQuery> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetAppByIdQuery requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
