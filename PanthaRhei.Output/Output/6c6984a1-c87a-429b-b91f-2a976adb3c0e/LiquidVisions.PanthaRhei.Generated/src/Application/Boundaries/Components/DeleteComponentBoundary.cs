using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Components;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Components
{
    internal class DeleteComponentBoundary : IBoundary<DeleteComponentCommand>
    {
        private readonly IInteractor<DeleteComponentCommand> interactor;

        public DeleteComponentBoundary(IInteractor<DeleteComponentCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(DeleteComponentCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
