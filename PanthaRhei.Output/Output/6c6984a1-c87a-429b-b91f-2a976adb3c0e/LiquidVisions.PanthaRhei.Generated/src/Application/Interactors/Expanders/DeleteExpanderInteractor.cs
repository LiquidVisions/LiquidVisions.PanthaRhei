using System;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.Expanders
{
    internal class DeleteExpanderInteractor : IInteractor<DeleteExpanderCommand>
    {
        private readonly IValidator<DeleteExpanderCommand> validator;
        private readonly IDeleteGateway<Expander> repository;
        private readonly IGetByIdGateway<Expander> getByIdRepository;

        public DeleteExpanderInteractor(
            IValidator<DeleteExpanderCommand> validator,
            IDeleteGateway<Expander> repository,
            IGetByIdGateway<Expander> getByIdRepository)
        {
            this.validator = validator;
            this.repository = repository;
            this.getByIdRepository = getByIdRepository;
        }

        public async Task<Response> ExecuteUseCase(DeleteExpanderCommand requestModel)
        {
            Response response = validator.Validate(requestModel);
            if (response.IsValid)
            {
                try
                {
                    Expander entity = getByIdRepository.GetById(requestModel.Id);
                    if (entity == null)
                    {
                        response.AddError(ErrorCodes.NotFound, $"Expander resource ({requestModel.Id}) not found.");
                        return response;
                    }

                    bool deleted = await repository.Delete(entity);
                    response.SetParameter(entity);
                    if (!deleted)
                    {
                        response.AddError(ErrorCodes.InternalServerError, $"Failed to delete entity id {requestModel.Id}");
                    }
                }
                catch (Exception exception)
                {
                    response.AddError(ErrorCodes.InternalServerError, exception.Message);
                }
            }

            return response;
        }
    }
}
