using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Expanders
{
    internal class GetExpanderByIdBoundary : IBoundary<GetExpanderByIdRequestModel>
    {
        private readonly IInteractor<GetExpanderByIdRequestModel> interactor;

        public GetExpanderByIdBoundary(IInteractor<GetExpanderByIdRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetExpanderByIdRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
