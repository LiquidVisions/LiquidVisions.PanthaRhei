using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Fields;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Fields
{
    internal class UpdateFieldBoundary : IBoundary<UpdateFieldRequestModel>
    {
        private readonly IInteractor<UpdateFieldRequestModel> interactor;

        public UpdateFieldBoundary(IInteractor<UpdateFieldRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(UpdateFieldRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
