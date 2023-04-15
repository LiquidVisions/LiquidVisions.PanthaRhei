using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.ConnectionStrings;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.ConnectionStrings
{
    internal class GetConnectionStringsBoundary : IBoundary<GetConnectionStringsRequestModel>
    {
        private readonly IInteractor<GetConnectionStringsRequestModel> interactor;

        public GetConnectionStringsBoundary(IInteractor<GetConnectionStringsRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetConnectionStringsRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
