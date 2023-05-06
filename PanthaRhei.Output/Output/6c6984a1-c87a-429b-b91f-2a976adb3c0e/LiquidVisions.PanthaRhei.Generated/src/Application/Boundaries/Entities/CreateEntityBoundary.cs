using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Entities
{
    internal class CreateEntityBoundary : IBoundary<CreateEntityRequestModel>
    {
        private readonly IInteractor<CreateEntityRequestModel> interactor;

        public CreateEntityBoundary(IInteractor<CreateEntityRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(CreateEntityRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
