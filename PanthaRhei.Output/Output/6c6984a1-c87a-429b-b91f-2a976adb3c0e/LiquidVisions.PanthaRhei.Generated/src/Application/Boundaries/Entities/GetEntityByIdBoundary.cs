using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Entities
{
    internal class GetEntityByIdBoundary : IBoundary<GetEntityByIdRequestModel>
    {
        private readonly IInteractor<GetEntityByIdRequestModel> interactor;

        public GetEntityByIdBoundary(IInteractor<GetEntityByIdRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetEntityByIdRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
