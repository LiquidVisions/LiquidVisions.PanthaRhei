using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Fields;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Fields
{
    internal class GetFieldsBoundary : IBoundary<GetFieldsRequestModel>
    {
        private readonly IInteractor<GetFieldsRequestModel> interactor;

        public GetFieldsBoundary(IInteractor<GetFieldsRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetFieldsRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
