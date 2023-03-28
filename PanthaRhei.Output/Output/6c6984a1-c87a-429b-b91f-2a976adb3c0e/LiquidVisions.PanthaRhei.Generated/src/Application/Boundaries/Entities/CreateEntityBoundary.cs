using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Entities
{
    internal class CreateEntityBoundary : IBoundary<CreateEntityCommand>
    {
        private readonly IInteractor<CreateEntityCommand> interactor;

        public CreateEntityBoundary(IInteractor<CreateEntityCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(CreateEntityCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
