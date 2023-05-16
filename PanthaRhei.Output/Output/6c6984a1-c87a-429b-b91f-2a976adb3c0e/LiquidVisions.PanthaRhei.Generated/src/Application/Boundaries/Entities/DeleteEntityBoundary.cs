using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Entities
{
    internal class DeleteEntityBoundary : IBoundary<DeleteEntityRequestModel>
    {
        private readonly IInteractor<DeleteEntityRequestModel> interactor;

        public DeleteEntityBoundary(IInteractor<DeleteEntityRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(DeleteEntityRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
