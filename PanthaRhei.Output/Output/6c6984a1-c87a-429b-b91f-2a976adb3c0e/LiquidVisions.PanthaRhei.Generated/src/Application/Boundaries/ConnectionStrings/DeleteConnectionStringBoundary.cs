using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.ConnectionStrings;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.ConnectionStrings
{
    internal class DeleteConnectionStringBoundary : IBoundary<DeleteConnectionStringRequestModel>
    {
        private readonly IInteractor<DeleteConnectionStringRequestModel> interactor;

        public DeleteConnectionStringBoundary(IInteractor<DeleteConnectionStringRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(DeleteConnectionStringRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
