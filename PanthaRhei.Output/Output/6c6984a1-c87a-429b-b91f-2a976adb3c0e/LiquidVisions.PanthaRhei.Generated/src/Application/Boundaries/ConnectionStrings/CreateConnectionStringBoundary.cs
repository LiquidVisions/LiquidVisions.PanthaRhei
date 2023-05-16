using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.ConnectionStrings;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.ConnectionStrings
{
    internal class CreateConnectionStringBoundary : IBoundary<CreateConnectionStringRequestModel>
    {
        private readonly IInteractor<CreateConnectionStringRequestModel> interactor;

        public CreateConnectionStringBoundary(IInteractor<CreateConnectionStringRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(CreateConnectionStringRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
