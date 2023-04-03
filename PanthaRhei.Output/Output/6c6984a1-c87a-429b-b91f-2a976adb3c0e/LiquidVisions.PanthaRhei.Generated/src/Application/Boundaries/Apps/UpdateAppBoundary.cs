using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Apps
{
    internal class UpdateAppBoundary : IBoundary<UpdateAppCommand>
    {
        private readonly IInteractor<UpdateAppCommand> interactor;

        public UpdateAppBoundary(IInteractor<UpdateAppCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(UpdateAppCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
