using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Fields;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Fields
{
    internal class GetFieldByIdBoundary : IBoundary<GetFieldByIdQuery>
    {
        private readonly IInteractor<GetFieldByIdQuery> interactor;

        public GetFieldByIdBoundary(IInteractor<GetFieldByIdQuery> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(GetFieldByIdQuery requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
