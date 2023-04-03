using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Expanders
{
    internal class GetExpanderByIdBoundary : IBoundary<GetExpanderByIdQuery>
    {
        private readonly IInteractor<GetExpanderByIdQuery> interactor;

        public GetExpanderByIdBoundary(IInteractor<GetExpanderByIdQuery> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetExpanderByIdQuery requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
