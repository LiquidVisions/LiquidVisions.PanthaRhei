using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Relationships
{
    internal class UpdateRelationshipBoundary : IBoundary<UpdateRelationshipCommand>
    {
        private readonly IInteractor<UpdateRelationshipCommand> interactor;

        public UpdateRelationshipBoundary(IInteractor<UpdateRelationshipCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(UpdateRelationshipCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
