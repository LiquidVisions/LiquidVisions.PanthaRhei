using System;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.Entities
{
    internal class UpdateEntityInteractor : IInteractor<UpdateEntityRequestModel>
    {
        private readonly IValidator<UpdateEntityRequestModel> validator;
        private readonly IMapper<UpdateEntityRequestModel, Entity> mapper;
        private readonly IUpdateGateway<Entity> repository;
        private readonly IGetByIdGateway<Entity> getRepository;

        public UpdateEntityInteractor(
            IValidator<UpdateEntityRequestModel> validator,
            IMapper<UpdateEntityRequestModel, Entity> mapper,
            IUpdateGateway<Entity> repository,
            IGetByIdGateway<Entity> getRepository)
        {
            this.validator = validator;
            this.mapper = mapper;
            this.repository = repository;
            this.getRepository = getRepository;
        }

        public async Task<Response> ExecuteUseCase(UpdateEntityRequestModel requestModel)
        {
            Response response = validator.Validate(requestModel);
            if (response.IsValid)
            {
                try
                {
                    Entity entity = getRepository.GetById(requestModel.Id);
                    if (entity == null)
                    {
                        response.AddError(ErrorCodes.NotFound, $"Resource {nameof(Entity)} with id {requestModel.Id} not found.");
                        return response;
                    }

                    mapper.Map(requestModel, entity);
                    response.SetParameter(entity);

                    int repositoryResult = await repository.Update(entity);
                    if (repositoryResult != 1)
                    {
                        response.AddError(ErrorCodes.InternalServerError, $"Failed to create {nameof(Entity)}.");
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
