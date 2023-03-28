using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Fields;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Fields
{
    internal class GetFieldsBoundary : IBoundary<GetFieldsQuery>
    {
        private readonly IInteractor<GetFieldsQuery> interactor;

        public GetFieldsBoundary(IInteractor<GetFieldsQuery> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetFieldsQuery requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
