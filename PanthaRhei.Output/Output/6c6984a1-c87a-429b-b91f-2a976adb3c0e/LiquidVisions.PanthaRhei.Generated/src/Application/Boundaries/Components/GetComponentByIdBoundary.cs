using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Components
{
    internal class GetComponentByIdBoundary : IBoundary<GetComponentByIdQuery>
    {
        private readonly IInteractor<GetComponentByIdQuery> interactor;

        public GetComponentByIdBoundary(IInteractor<GetComponentByIdQuery> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetComponentByIdQuery requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
