using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Expanders
{
    internal class CreateExpanderBoundary : IBoundary<CreateExpanderCommand>
    {
        private readonly IInteractor<CreateExpanderCommand> interactor;

        public CreateExpanderBoundary(IInteractor<CreateExpanderCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(CreateExpanderCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
