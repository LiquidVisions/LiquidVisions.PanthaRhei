using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Expanders
{
    internal class GetExpandersBoundary : IBoundary<GetExpandersQuery>
    {
        private readonly IInteractor<GetExpandersQuery> interactor;

        public GetExpandersBoundary(IInteractor<GetExpandersQuery> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetExpandersQuery requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
