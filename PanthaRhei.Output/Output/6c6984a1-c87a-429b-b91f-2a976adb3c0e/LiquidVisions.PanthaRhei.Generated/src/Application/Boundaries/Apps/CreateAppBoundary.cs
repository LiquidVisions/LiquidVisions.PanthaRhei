using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Apps
{
    internal class CreateAppBoundary : IBoundary<CreateAppRequestModel>
    {
        private readonly IInteractor<CreateAppRequestModel> interactor;

        public CreateAppBoundary(IInteractor<CreateAppRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(CreateAppRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
