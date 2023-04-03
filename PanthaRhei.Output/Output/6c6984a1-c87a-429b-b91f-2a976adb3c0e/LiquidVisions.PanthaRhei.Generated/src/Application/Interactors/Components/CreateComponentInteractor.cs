using System;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.Components
{
    internal class CreateComponentInteractor : IInteractor<CreateComponentCommand>
    {
        private readonly IValidator<CreateComponentCommand> validator;
        private readonly IMapper<CreateComponentCommand, Component> mapper;
        private readonly ICreateGateway<Component> repository;

        public CreateComponentInteractor(
            IValidator<CreateComponentCommand> validator,
            IMapper<CreateComponentCommand, Component> mapper,
            ICreateGateway<Component> gateway)
        {
            this.validator = validator;
            this.mapper = mapper;
            repository = gateway;
        }

        public async Task<Response> ExecuteUseCase(CreateComponentCommand requestModel)
        {
            Response result = validator.Validate(requestModel);
            if (result.IsValid)
            {
                try
                {
                    Component entity = mapper.Map(requestModel);
                    entity.Id = Guid.NewGuid();

                    result.SetParameter(entity);

                    int repositoryResult = await repository.Create(entity);
                    if (repositoryResult != 1)
                    {
                        result.AddError(ErrorCodes.InternalServerError, $"Failed to create {nameof(Component)}.");
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
