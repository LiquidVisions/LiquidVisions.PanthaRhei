using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Entities
{
    internal class GetEntityByIdBoundary : IBoundary<GetEntityByIdQuery>
    {
        private readonly IInteractor<GetEntityByIdQuery> interactor;

        public GetEntityByIdBoundary(IInteractor<GetEntityByIdQuery> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetEntityByIdQuery requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
