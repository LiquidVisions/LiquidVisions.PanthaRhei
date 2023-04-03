using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Apps
{
    internal class CreateAppBoundary : IBoundary<CreateAppCommand>
    {
        private readonly IInteractor<CreateAppCommand> interactor;

        public CreateAppBoundary(IInteractor<CreateAppCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(CreateAppCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
