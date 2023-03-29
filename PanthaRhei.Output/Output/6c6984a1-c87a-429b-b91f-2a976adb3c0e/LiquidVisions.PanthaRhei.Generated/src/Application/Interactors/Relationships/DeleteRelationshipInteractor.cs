using System;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Relationships;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.Relationships
{
    internal class DeleteRelationshipInteractor : IInteractor<DeleteRelationshipCommand>
    {
        private readonly IValidator<DeleteRelationshipCommand> validator;
        private readonly IDeleteGateway<Relationship> repository;
        private readonly IGetByIdGateway<Relationship> getByIdRepository;

        public DeleteRelationshipInteractor(
            IValidator<DeleteRelationshipCommand> validator,
            IDeleteGateway<Relationship> repository,
            IGetByIdGateway<Relationship> getByIdRepository)
        {
            this.validator = validator;
            this.repository = repository;
            this.getByIdRepository = getByIdRepository;
        }

        public async Task<Response> ExecuteUseCase(DeleteRelationshipCommand requestModel)
        {
            Response response = validator.Validate(requestModel);
            if (response.IsValid)
            {
                try
                {
                    Relationship entity = getByIdRepository.GetById(requestModel.Id);
                    if (entity == null)
                    {
                        response.AddError(ErrorCodes.NotFound, $"Relationship resource ({requestModel.Id}) not found.");
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
