using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Components
{
    internal class GetComponentByIdBoundary : IBoundary<GetComponentByIdRequestModel>
    {
        private readonly IInteractor<GetComponentByIdRequestModel> interactor;

        public GetComponentByIdBoundary(IInteractor<GetComponentByIdRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetComponentByIdRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
