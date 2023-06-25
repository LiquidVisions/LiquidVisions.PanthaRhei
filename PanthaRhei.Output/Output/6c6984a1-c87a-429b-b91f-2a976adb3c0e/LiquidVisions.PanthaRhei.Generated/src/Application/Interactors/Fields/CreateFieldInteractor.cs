using System;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Fields;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.Fields
{
    internal class CreateFieldInteractor : IInteractor<CreateFieldRequestModel>
    {
        private readonly IValidator<CreateFieldRequestModel> validator;
        private readonly IMapper<CreateFieldRequestModel, Field> mapper;
        private readonly ICreateGateway<Field> repository;

        public CreateFieldInteractor(
            IValidator<CreateFieldRequestModel> validator,
            IMapper<CreateFieldRequestModel, Field> mapper,
            ICreateGateway<Field> gateway)
        {
            this.validator = validator;
            this.mapper = mapper;
            repository = gateway;
        }

        public async Task<Response> ExecuteUseCase(CreateFieldRequestModel requestModel)
        {
            Response result = validator.Validate(requestModel);
            if (result.IsValid)
            {
                try
                {
                    Field entity = mapper.Map(requestModel);
                    entity.Id = Guid.NewGuid();

                    result.SetParameter(entity);

                    int repositoryResult = await repository.Create(entity);
                    if (repositoryResult < 1)
                    {
                        result.AddError(ErrorCodes.InternalServerError, $"Failed to create {nameof(Field)}.");
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
