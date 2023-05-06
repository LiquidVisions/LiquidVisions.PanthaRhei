using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Relationships
{
    internal class GetRelationshipsBoundary : IBoundary<GetRelationshipsRequestModel>
    {
        private readonly IInteractor<GetRelationshipsRequestModel> interactor;

        public GetRelationshipsBoundary(IInteractor<GetRelationshipsRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetRelationshipsRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
