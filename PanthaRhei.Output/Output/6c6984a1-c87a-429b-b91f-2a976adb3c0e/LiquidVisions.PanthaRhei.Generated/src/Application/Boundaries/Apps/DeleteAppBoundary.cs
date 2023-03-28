using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Apps;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Apps
{
    internal class DeleteAppBoundary : IBoundary<DeleteAppCommand>
    {
        private readonly IInteractor<DeleteAppCommand> interactor;

        public DeleteAppBoundary(IInteractor<DeleteAppCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(DeleteAppCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
