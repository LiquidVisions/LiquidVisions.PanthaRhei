using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Fields;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Fields
{
    internal class GetFieldByIdBoundary : IBoundary<GetFieldByIdRequestModel>
    {
        private readonly IInteractor<GetFieldByIdRequestModel> interactor;

        public GetFieldByIdBoundary(IInteractor<GetFieldByIdRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetFieldByIdRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
