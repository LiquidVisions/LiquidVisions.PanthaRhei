using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Relationships
{
    internal class UpdateRelationshipBoundary : IBoundary<UpdateRelationshipRequestModel>
    {
        private readonly IInteractor<UpdateRelationshipRequestModel> interactor;

        public UpdateRelationshipBoundary(IInteractor<UpdateRelationshipRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(UpdateRelationshipRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
