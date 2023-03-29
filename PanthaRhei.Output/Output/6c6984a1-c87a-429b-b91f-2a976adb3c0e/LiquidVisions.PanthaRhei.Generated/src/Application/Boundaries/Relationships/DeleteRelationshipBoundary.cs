using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Relationships;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Relationships
{
    internal class DeleteRelationshipBoundary : IBoundary<DeleteRelationshipCommand>
    {
        private readonly IInteractor<DeleteRelationshipCommand> interactor;

        public DeleteRelationshipBoundary(IInteractor<DeleteRelationshipCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(DeleteRelationshipCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
