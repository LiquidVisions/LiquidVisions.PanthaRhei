using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Relationships;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Relationships
{
    internal class GetRelationshipsBoundary : IBoundary<GetRelationshipsQuery>
    {
        private readonly IInteractor<GetRelationshipsQuery> interactor;

        public GetRelationshipsBoundary(IInteractor<GetRelationshipsQuery> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetRelationshipsQuery requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
