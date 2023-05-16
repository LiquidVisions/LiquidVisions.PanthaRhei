using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Relationships
{
    internal class GetRelationshipByIdBoundary : IBoundary<GetRelationshipByIdRequestModel>
    {
        private readonly IInteractor<GetRelationshipByIdRequestModel> interactor;

        public GetRelationshipByIdBoundary(IInteractor<GetRelationshipByIdRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetRelationshipByIdRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
