using System;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.ConnectionStrings;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.ConnectionStrings
{
    internal class DeleteConnectionStringInteractor : IInteractor<DeleteConnectionStringCommand>
    {
        private readonly IValidator<DeleteConnectionStringCommand> validator;
        private readonly IDeleteGateway<ConnectionString> repository;
        private readonly IGetByIdGateway<ConnectionString> getByIdRepository;

        public DeleteConnectionStringInteractor(
            IValidator<DeleteConnectionStringCommand> validator,
            IDeleteGateway<ConnectionString> repository,
            IGetByIdGateway<ConnectionString> getByIdRepository)
        {
            this.validator = validator;
            this.repository = repository;
            this.getByIdRepository = getByIdRepository;
        }

        public async Task<Response> ExecuteUseCase(DeleteConnectionStringCommand requestModel)
        {
            Response response = validator.Validate(requestModel);
            if (response.IsValid)
            {
                try
                {
                    ConnectionString entity = getByIdRepository.GetById(requestModel.Id);
                    if (entity == null)
                    {
                        response.AddError(ErrorCodes.NotFound, $"ConnectionString resource ({requestModel.Id}) not found.");
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
