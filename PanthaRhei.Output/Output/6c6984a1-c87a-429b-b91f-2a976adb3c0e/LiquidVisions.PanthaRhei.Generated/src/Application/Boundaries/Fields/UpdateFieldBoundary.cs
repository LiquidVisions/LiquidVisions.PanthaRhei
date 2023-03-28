using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Fields;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Fields
{
    internal class UpdateFieldBoundary : IBoundary<UpdateFieldCommand>
    {
        private readonly IInteractor<UpdateFieldCommand> interactor;

        public UpdateFieldBoundary(IInteractor<UpdateFieldCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(UpdateFieldCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
