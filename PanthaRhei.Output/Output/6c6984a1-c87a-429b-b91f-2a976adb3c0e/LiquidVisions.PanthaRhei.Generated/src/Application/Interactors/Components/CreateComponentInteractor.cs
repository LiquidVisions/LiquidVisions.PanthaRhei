using System;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.Components
{
    internal class CreateComponentInteractor : IInteractor<CreateComponentRequestModel>
    {
        private readonly IValidator<CreateComponentRequestModel> validator;
        private readonly IMapper<CreateComponentRequestModel, Component> mapper;
        private readonly ICreateGateway<Component> repository;

        public CreateComponentInteractor(
            IValidator<CreateComponentRequestModel> validator,
            IMapper<CreateComponentRequestModel, Component> mapper,
            ICreateGateway<Component> gateway)
        {
            this.validator = validator;
            this.mapper = mapper;
            repository = gateway;
        }

        public async Task<Response> ExecuteUseCase(CreateComponentRequestModel requestModel)
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
