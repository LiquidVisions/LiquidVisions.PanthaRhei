using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Components
{
    internal class UpdateComponentBoundary : IBoundary<UpdateComponentCommand>
    {
        private readonly IInteractor<UpdateComponentCommand> interactor;

        public UpdateComponentBoundary(IInteractor<UpdateComponentCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(UpdateComponentCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
