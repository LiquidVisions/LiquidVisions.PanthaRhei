using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Expanders
{
    internal class DeleteExpanderBoundary : IBoundary<DeleteExpanderRequestModel>
    {
        private readonly IInteractor<DeleteExpanderRequestModel> interactor;

        public DeleteExpanderBoundary(IInteractor<DeleteExpanderRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(DeleteExpanderRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
