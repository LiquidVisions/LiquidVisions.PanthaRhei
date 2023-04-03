using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Fields;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Fields
{
    internal class DeleteFieldBoundary : IBoundary<DeleteFieldCommand>
    {
        private readonly IInteractor<DeleteFieldCommand> interactor;

        public DeleteFieldBoundary(IInteractor<DeleteFieldCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(DeleteFieldCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
