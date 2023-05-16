using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Expanders
{
    internal class UpdateExpanderBoundary : IBoundary<UpdateExpanderRequestModel>
    {
        private readonly IInteractor<UpdateExpanderRequestModel> interactor;

        public UpdateExpanderBoundary(IInteractor<UpdateExpanderRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(UpdateExpanderRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
