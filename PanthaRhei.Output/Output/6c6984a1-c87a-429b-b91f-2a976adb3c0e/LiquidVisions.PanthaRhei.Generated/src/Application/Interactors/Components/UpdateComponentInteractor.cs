using System;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.Components
{
    internal class UpdateComponentInteractor : IInteractor<UpdateComponentCommand>
    {
        private readonly IValidator<UpdateComponentCommand> validator;
        private readonly IMapper<UpdateComponentCommand, Component> mapper;
        private readonly IUpdateGateway<Component> repository;
        private readonly IGetByIdGateway<Component> getRepository;

        public UpdateComponentInteractor(
            IValidator<UpdateComponentCommand> validator,
            IMapper<UpdateComponentCommand, Component> mapper,
            IUpdateGateway<Component> repository,
            IGetByIdGateway<Component> getRepository)
        {
            this.validator = validator;
            this.mapper = mapper;
            this.repository = repository;
            this.getRepository = getRepository;
        }

        public async Task<Response> ExecuteUseCase(UpdateComponentCommand requestModel)
        {
            Response response = validator.Validate(requestModel);
            if (response.IsValid)
            {
                try
                {
                    Component entity = getRepository.GetById(requestModel.Id);
                    if (entity == null)
                    {
                        response.AddError(ErrorCodes.NotFound, $"Resource {nameof(Component)} with id {requestModel.Id} not found.");
                        return response;
                    }

                    mapper.Map(requestModel, entity);
                    response.SetParameter(entity);

                    int repositoryResult = await repository.Update(entity);
                    if (repositoryResult != 1)
                    {
                        response.AddError(ErrorCodes.InternalServerError, $"Failed to create {nameof(Component)}.");
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
