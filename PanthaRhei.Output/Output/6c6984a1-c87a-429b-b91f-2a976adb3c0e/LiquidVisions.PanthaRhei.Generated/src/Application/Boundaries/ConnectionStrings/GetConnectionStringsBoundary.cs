using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.ConnectionStrings;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.ConnectionStrings
{
    internal class GetConnectionStringsBoundary : IBoundary<GetConnectionStringsQuery>
    {
        private readonly IInteractor<GetConnectionStringsQuery> interactor;

        public GetConnectionStringsBoundary(IInteractor<GetConnectionStringsQuery> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetConnectionStringsQuery requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
