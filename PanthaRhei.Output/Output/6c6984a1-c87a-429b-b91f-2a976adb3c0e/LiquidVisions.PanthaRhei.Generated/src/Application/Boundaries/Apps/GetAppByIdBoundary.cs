using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Apps
{
    internal class GetAppByIdBoundary : IBoundary<GetAppByIdRequestModel>
    {
        private readonly IInteractor<GetAppByIdRequestModel> interactor;

        public GetAppByIdBoundary(IInteractor<GetAppByIdRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetAppByIdRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
