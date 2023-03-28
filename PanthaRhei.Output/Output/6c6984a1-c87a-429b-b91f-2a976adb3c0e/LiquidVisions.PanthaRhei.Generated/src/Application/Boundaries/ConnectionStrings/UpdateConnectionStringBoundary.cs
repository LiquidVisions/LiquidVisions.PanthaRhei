using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.ConnectionStrings;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.ConnectionStrings
{
    internal class UpdateConnectionStringBoundary : IBoundary<UpdateConnectionStringCommand>
    {
        private readonly IInteractor<UpdateConnectionStringCommand> interactor;

        public UpdateConnectionStringBoundary(IInteractor<UpdateConnectionStringCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(UpdateConnectionStringCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
