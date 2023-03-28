using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.ConnectionStrings;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.ConnectionStrings
{
    internal class GetConnectionStringByIdBoundary : IBoundary<GetConnectionStringByIdQuery>
    {
        private readonly IInteractor<GetConnectionStringByIdQuery> interactor;

        public GetConnectionStringByIdBoundary(IInteractor<GetConnectionStringByIdQuery> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetConnectionStringByIdQuery requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
