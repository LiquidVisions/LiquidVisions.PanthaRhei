using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Components
{
    internal class UpdateComponentBoundary : IBoundary<UpdateComponentRequestModel>
    {
        private readonly IInteractor<UpdateComponentRequestModel> interactor;

        public UpdateComponentBoundary(IInteractor<UpdateComponentRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(UpdateComponentRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
