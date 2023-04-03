using System;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.Components
{
    internal class DeleteComponentInteractor : IInteractor<DeleteComponentCommand>
    {
        private readonly IValidator<DeleteComponentCommand> validator;
        private readonly IDeleteGateway<Component> repository;
        private readonly IGetByIdGateway<Component> getByIdRepository;

        public DeleteComponentInteractor(
            IValidator<DeleteComponentCommand> validator,
            IDeleteGateway<Component> repository,
            IGetByIdGateway<Component> getByIdRepository)
        {
            this.validator = validator;
            this.repository = repository;
            this.getByIdRepository = getByIdRepository;
        }

        public async Task<Response> ExecuteUseCase(DeleteComponentCommand requestModel)
        {
            Response response = validator.Validate(requestModel);
            if (response.IsValid)
            {
                try
                {
                    Component entity = getByIdRepository.GetById(requestModel.Id);
                    if (entity == null)
                    {
                        response.AddError(ErrorCodes.NotFound, $"Component resource ({requestModel.Id}) not found.");
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
