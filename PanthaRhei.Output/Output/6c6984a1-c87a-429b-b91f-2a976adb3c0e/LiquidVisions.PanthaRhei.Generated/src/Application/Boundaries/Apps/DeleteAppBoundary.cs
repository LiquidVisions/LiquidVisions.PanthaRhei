using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Apps
{
    internal class DeleteAppBoundary : IBoundary<DeleteAppRequestModel>
    {
        private readonly IInteractor<DeleteAppRequestModel> interactor;

        public DeleteAppBoundary(IInteractor<DeleteAppRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(DeleteAppRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
