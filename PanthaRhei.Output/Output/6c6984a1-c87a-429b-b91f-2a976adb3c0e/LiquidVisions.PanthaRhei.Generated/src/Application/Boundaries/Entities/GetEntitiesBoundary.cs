using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Entities
{
    internal class GetEntitiesBoundary : IBoundary<GetEntitiesQuery>
    {
        private readonly IInteractor<GetEntitiesQuery> interactor;

        public GetEntitiesBoundary(IInteractor<GetEntitiesQuery> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetEntitiesQuery requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
