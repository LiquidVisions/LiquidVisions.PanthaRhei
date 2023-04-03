using System;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.Entities
{
    internal class CreateEntityInteractor : IInteractor<CreateEntityCommand>
    {
        private readonly IValidator<CreateEntityCommand> validator;
        private readonly IMapper<CreateEntityCommand, Entity> mapper;
        private readonly ICreateGateway<Entity> repository;

        public CreateEntityInteractor(
            IValidator<CreateEntityCommand> validator,
            IMapper<CreateEntityCommand, Entity> mapper,
            ICreateGateway<Entity> gateway)
        {
            this.validator = validator;
            this.mapper = mapper;
            repository = gateway;
        }

        public async Task<Response> ExecuteUseCase(CreateEntityCommand requestModel)
        {
            Response result = validator.Validate(requestModel);
            if (result.IsValid)
            {
                try
                {
                    Entity entity = mapper.Map(requestModel);
                    entity.Id = Guid.NewGuid();

                    result.SetParameter(entity);

                    int repositoryResult = await repository.Create(entity);
                    if (repositoryResult != 1)
                    {
                        result.AddError(ErrorCodes.InternalServerError, $"Failed to create {nameof(Entity)}.");
                        return result;
                    }
                }
                catch (Exception exception)
                {
                    result.AddError(ErrorCodes.InternalServerError, exception.Message);
                }
            }

            return result;
        }
    }
}
