using System;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Fields;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.Fields
{
    internal class DeleteFieldInteractor : IInteractor<DeleteFieldCommand>
    {
        private readonly IValidator<DeleteFieldCommand> validator;
        private readonly IDeleteGateway<Field> repository;
        private readonly IGetByIdGateway<Field> getByIdRepository;

        public DeleteFieldInteractor(
            IValidator<DeleteFieldCommand> validator,
            IDeleteGateway<Field> repository,
            IGetByIdGateway<Field> getByIdRepository)
        {
            this.validator = validator;
            this.repository = repository;
            this.getByIdRepository = getByIdRepository;
        }

        public async Task<Response> ExecuteUseCase(DeleteFieldCommand requestModel)
        {
            Response response = validator.Validate(requestModel);
            if (response.IsValid)
            {
                try
                {
                    Field entity = getByIdRepository.GetById(requestModel.Id);
                    if (entity == null)
                    {
                        response.AddError(ErrorCodes.NotFound, $"Field resource ({requestModel.Id}) not found.");
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
