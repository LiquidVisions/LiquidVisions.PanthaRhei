using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.ConnectionStrings;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.ConnectionStrings
{
    internal class UpdateConnectionStringBoundary : IBoundary<UpdateConnectionStringRequestModel>
    {
        private readonly IInteractor<UpdateConnectionStringRequestModel> interactor;

        public UpdateConnectionStringBoundary(IInteractor<UpdateConnectionStringRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(UpdateConnectionStringRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
