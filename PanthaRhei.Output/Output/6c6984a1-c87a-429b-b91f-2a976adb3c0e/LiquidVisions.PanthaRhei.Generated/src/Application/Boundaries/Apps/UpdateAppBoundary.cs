using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Apps
{
    internal class UpdateAppBoundary : IBoundary<UpdateAppRequestModel>
    {
        private readonly IInteractor<UpdateAppRequestModel> interactor;

        public UpdateAppBoundary(IInteractor<UpdateAppRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(UpdateAppRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
