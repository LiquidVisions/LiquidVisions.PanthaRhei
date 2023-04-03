using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Components
{
    internal class CreateComponentBoundary : IBoundary<CreateComponentCommand>
    {
        private readonly IInteractor<CreateComponentCommand> interactor;

        public CreateComponentBoundary(IInteractor<CreateComponentCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(CreateComponentCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
