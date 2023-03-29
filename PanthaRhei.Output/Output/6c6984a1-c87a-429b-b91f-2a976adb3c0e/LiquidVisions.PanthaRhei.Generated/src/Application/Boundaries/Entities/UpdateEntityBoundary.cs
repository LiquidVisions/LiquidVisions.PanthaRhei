using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Entities
{
    internal class UpdateEntityBoundary : IBoundary<UpdateEntityCommand>
    {
        private readonly IInteractor<UpdateEntityCommand> interactor;

        public UpdateEntityBoundary(IInteractor<UpdateEntityCommand> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(UpdateEntityCommand requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
