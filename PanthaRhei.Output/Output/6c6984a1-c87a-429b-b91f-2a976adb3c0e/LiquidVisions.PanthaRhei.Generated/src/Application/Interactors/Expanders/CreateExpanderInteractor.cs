using System;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.Expanders
{
    internal class CreateExpanderInteractor : IInteractor<CreateExpanderRequestModel>
    {
        private readonly IValidator<CreateExpanderRequestModel> validator;
        private readonly IMapper<CreateExpanderRequestModel, Expander> mapper;
        private readonly ICreateGateway<Expander> repository;

        public CreateExpanderInteractor(
            IValidator<CreateExpanderRequestModel> validator,
            IMapper<CreateExpanderRequestModel, Expander> mapper,
            ICreateGateway<Expander> gateway)
        {
            this.validator = validator;
            this.mapper = mapper;
            repository = gateway;
        }

        public async Task<Response> ExecuteUseCase(CreateExpanderRequestModel requestModel)
        {
            Response result = validator.Validate(requestModel);
            if (result.IsValid)
            {
                try
                {
                    Expander entity = mapper.Map(requestModel);
                    entity.Id = Guid.NewGuid();

                    result.SetParameter(entity);

                    int repositoryResult = await repository.Create(entity);
                    if (repositoryResult != 1)
                    {
                        result.AddError(ErrorCodes.InternalServerError, $"Failed to create {nameof(Expander)}.");
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
