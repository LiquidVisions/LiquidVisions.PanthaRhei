using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Components
{
    internal class CreateComponentBoundary : IBoundary<CreateComponentRequestModel>
    {
        private readonly IInteractor<CreateComponentRequestModel> interactor;

        public CreateComponentBoundary(IInteractor<CreateComponentRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(CreateComponentRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
