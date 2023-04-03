using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Entities
{
    internal class DeleteEntityBoundary : IBoundary<DeleteEntityCommand>
    {
        private readonly IInteractor<DeleteEntityCommand> interactor;

        public DeleteEntityBoundary(IInteractor<DeleteEntityCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(DeleteEntityCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
