using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Relationships
{
    internal class CreateRelationshipBoundary : IBoundary<CreateRelationshipRequestModel>
    {
        private readonly IInteractor<CreateRelationshipRequestModel> interactor;

        public CreateRelationshipBoundary(IInteractor<CreateRelationshipRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(CreateRelationshipRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
