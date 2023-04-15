using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Expanders
{
    internal class CreateExpanderBoundary : IBoundary<CreateExpanderRequestModel>
    {
        private readonly IInteractor<CreateExpanderRequestModel> interactor;

        public CreateExpanderBoundary(IInteractor<CreateExpanderRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(CreateExpanderRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
