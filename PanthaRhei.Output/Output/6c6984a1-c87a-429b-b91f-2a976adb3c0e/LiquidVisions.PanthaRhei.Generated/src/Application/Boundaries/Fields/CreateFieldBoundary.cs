using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Fields;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Fields
{
    internal class CreateFieldBoundary : IBoundary<CreateFieldCommand>
    {
        private readonly IInteractor<CreateFieldCommand> interactor;

        public CreateFieldBoundary(IInteractor<CreateFieldCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(CreateFieldCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
