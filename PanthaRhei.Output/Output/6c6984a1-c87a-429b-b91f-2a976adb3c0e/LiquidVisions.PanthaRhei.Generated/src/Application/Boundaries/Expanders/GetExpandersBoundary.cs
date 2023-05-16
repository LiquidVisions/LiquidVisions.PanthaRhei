using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Expanders
{
    internal class GetExpandersBoundary : IBoundary<GetExpandersRequestModel>
    {
        private readonly IInteractor<GetExpandersRequestModel> interactor;

        public GetExpandersBoundary(IInteractor<GetExpandersRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetExpandersRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
