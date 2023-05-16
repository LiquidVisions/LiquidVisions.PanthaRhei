using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Relationships
{
    internal class DeleteRelationshipBoundary : IBoundary<DeleteRelationshipRequestModel>
    {
        private readonly IInteractor<DeleteRelationshipRequestModel> interactor;

        public DeleteRelationshipBoundary(IInteractor<DeleteRelationshipRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(DeleteRelationshipRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
