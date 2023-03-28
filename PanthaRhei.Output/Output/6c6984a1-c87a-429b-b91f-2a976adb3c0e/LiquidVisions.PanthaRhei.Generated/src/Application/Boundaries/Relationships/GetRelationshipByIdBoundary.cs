using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Relationships;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Relationships
{
    internal class GetRelationshipByIdBoundary : IBoundary<GetRelationshipByIdQuery>
    {
        private readonly IInteractor<GetRelationshipByIdQuery> interactor;

        public GetRelationshipByIdBoundary(IInteractor<GetRelationshipByIdQuery> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetRelationshipByIdQuery requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
