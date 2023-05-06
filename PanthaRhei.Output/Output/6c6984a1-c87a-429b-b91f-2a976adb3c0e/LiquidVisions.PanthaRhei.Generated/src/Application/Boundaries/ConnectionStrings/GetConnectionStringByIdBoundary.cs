using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.ConnectionStrings;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.ConnectionStrings
{
    internal class GetConnectionStringByIdBoundary : IBoundary<GetConnectionStringByIdRequestModel>
    {
        private readonly IInteractor<GetConnectionStringByIdRequestModel> interactor;

        public GetConnectionStringByIdBoundary(IInteractor<GetConnectionStringByIdRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetConnectionStringByIdRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
