using System;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.Relationships
{
    internal class UpdateRelationshipInteractor : IInteractor<UpdateRelationshipRequestModel>
    {
        private readonly IValidator<UpdateRelationshipRequestModel> validator;
        private readonly IMapper<UpdateRelationshipRequestModel, Relationship> mapper;
        private readonly IUpdateGateway<Relationship> repository;
        private readonly IGetByIdGateway<Relationship> getRepository;

        public UpdateRelationshipInteractor(
            IValidator<UpdateRelationshipRequestModel> validator,
            IMapper<UpdateRelationshipRequestModel, Relationship> mapper,
            IUpdateGateway<Relationship> repository,
            IGetByIdGateway<Relationship> getRepository)
        {
            this.validator = validator;
            this.mapper = mapper;
            this.repository = repository;
            this.getRepository = getRepository;
        }

        public async Task<Response> ExecuteUseCase(UpdateRelationshipRequestModel requestModel)
        {
            Response response = validator.Validate(requestModel);
            if (response.IsValid)
            {
                try
                {
                    Relationship entity = getRepository.GetById(requestModel.Id);
                    if (entity == null)
                    {
                        response.AddError(ErrorCodes.NotFound, $"Resource {nameof(Relationship)} with id {requestModel.Id} not found.");
                        return response;
                    }

                    mapper.Map(requestModel, entity);
                    response.SetParameter(entity);

                    int repositoryResult = await repository.Update(entity);
                    if (repositoryResult != 1)
                    {
                        response.AddError(ErrorCodes.InternalServerError, $"Failed to create {nameof(Relationship)}.");
                        return response;
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
