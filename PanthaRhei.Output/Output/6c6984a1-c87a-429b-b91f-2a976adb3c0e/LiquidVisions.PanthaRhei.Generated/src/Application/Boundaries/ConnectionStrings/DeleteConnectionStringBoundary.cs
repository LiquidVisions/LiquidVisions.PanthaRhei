using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.ConnectionStrings;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.ConnectionStrings
{
    internal class DeleteConnectionStringBoundary : IBoundary<DeleteConnectionStringCommand>
    {
        private readonly IInteractor<DeleteConnectionStringCommand> interactor;

        public DeleteConnectionStringBoundary(IInteractor<DeleteConnectionStringCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(DeleteConnectionStringCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
