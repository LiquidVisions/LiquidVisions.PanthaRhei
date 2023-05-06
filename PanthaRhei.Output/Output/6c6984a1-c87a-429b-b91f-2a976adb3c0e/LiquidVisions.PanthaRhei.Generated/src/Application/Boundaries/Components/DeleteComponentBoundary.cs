using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Components
{
    internal class DeleteComponentBoundary : IBoundary<DeleteComponentRequestModel>
    {
        private readonly IInteractor<DeleteComponentRequestModel> interactor;

        public DeleteComponentBoundary(IInteractor<DeleteComponentRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(DeleteComponentRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
