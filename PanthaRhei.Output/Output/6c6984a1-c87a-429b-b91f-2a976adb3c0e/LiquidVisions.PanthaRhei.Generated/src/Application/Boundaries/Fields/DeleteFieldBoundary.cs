using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Fields;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Fields
{
    internal class DeleteFieldBoundary : IBoundary<DeleteFieldRequestModel>
    {
        private readonly IInteractor<DeleteFieldRequestModel> interactor;

        public DeleteFieldBoundary(IInteractor<DeleteFieldRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(DeleteFieldRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
