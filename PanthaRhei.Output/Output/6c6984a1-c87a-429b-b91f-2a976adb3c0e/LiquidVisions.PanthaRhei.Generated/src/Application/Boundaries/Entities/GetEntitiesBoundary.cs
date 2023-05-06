using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Entities
{
    internal class GetEntitiesBoundary : IBoundary<GetEntitiesRequestModel>
    {
        private readonly IInteractor<GetEntitiesRequestModel> interactor;

        public GetEntitiesBoundary(IInteractor<GetEntitiesRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetEntitiesRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
