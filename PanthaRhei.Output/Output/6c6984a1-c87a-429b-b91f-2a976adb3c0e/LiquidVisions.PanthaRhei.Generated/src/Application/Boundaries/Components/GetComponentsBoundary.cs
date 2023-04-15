using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Components
{
    internal class GetComponentsBoundary : IBoundary<GetComponentsRequestModel>
    {
        private readonly IInteractor<GetComponentsRequestModel> interactor;

        public GetComponentsBoundary(IInteractor<GetComponentsRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetComponentsRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
