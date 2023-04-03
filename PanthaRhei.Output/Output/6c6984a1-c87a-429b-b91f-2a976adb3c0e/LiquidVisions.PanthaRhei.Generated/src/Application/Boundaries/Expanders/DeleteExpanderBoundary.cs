using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Expanders
{
    internal class DeleteExpanderBoundary : IBoundary<DeleteExpanderCommand>
    {
        private readonly IInteractor<DeleteExpanderCommand> interactor;

        public DeleteExpanderBoundary(IInteractor<DeleteExpanderCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(DeleteExpanderCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
