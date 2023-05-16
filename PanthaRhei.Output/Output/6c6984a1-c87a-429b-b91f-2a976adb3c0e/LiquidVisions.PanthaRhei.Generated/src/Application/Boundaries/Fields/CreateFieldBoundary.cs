using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Interactors;
using LiquidVisions.PanthaRhei.Generated.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Fields;

namespace LiquidVisions.PanthaRhei.Generated.Application.Boundaries.Fields
{
    internal class CreateFieldBoundary : IBoundary<CreateFieldRequestModel>
    {
        private readonly IInteractor<CreateFieldRequestModel> interactor;

        public CreateFieldBoundary(IInteractor<CreateFieldRequestModel> interactor)
        {
            this.interactor = interactor;
        }

        public async Task Execute(CreateFieldRequestModel requestModel, IPresenter presenter) =>
            presenter.Response = await interactor.ExecuteUseCase(requestModel);
    }
}
