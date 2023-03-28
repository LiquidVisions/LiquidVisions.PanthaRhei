using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.ConnectionStrings;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.ConnectionStrings
{
    internal class CreateConnectionStringBoundary : IBoundary<CreateConnectionStringCommand>
    {
        private readonly IInteractor<CreateConnectionStringCommand> interactor;

        public CreateConnectionStringBoundary(IInteractor<CreateConnectionStringCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(CreateConnectionStringCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
