using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Components
{
    internal class GetComponentsBoundary : IBoundary<GetComponentsQuery>
    {
        private readonly IInteractor<GetComponentsQuery> interactor;

        public GetComponentsBoundary(IInteractor<GetComponentsQuery> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetComponentsQuery requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
