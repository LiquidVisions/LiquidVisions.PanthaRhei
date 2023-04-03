using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Expanders
{
    internal class UpdateExpanderBoundary : IBoundary<UpdateExpanderCommand>
    {
        private readonly IInteractor<UpdateExpanderCommand> interactor;

        public UpdateExpanderBoundary(IInteractor<UpdateExpanderCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(UpdateExpanderCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
