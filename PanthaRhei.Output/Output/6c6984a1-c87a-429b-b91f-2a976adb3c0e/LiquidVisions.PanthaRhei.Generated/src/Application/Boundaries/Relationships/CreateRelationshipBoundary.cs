using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Relationships
{
    internal class CreateRelationshipBoundary : IBoundary<CreateRelationshipCommand>
    {
        private readonly IInteractor<CreateRelationshipCommand> interactor;

        public CreateRelationshipBoundary(IInteractor<CreateRelationshipCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(CreateRelationshipCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
