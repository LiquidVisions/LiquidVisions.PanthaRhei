using System;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.Relationships
{
    internal class CreateRelationshipInteractor : IInteractor<CreateRelationshipRequestModel>
    {
        private readonly IValidator<CreateRelationshipRequestModel> validator;
        private readonly IMapper<CreateRelationshipRequestModel, Relationship> mapper;
        private readonly ICreateGateway<Relationship> repository;

        public CreateRelationshipInteractor(
            IValidator<CreateRelationshipRequestModel> validator,
            IMapper<CreateRelationshipRequestModel, Relationship> mapper,
            ICreateGateway<Relationship> gateway)
        {
            this.validator = validator;
            this.mapper = mapper;
            repository = gateway;
        }

        public async Task<Response> ExecuteUseCase(CreateRelationshipRequestModel requestModel)
        {
            Response result = validator.Validate(requestModel);
            if (result.IsValid)
            {
                try
                {
                    Relationship entity = mapper.Map(requestModel);
                    entity.Id = Guid.NewGuid();

                    result.SetParameter(entity);

                    int repositoryResult = await repository.Create(entity);
                    if (repositoryResult < 1)
                    {
                        result.AddError(ErrorCodes.InternalServerError, $"Failed to create {nameof(Relationship)}.");
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
