using System;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.Apps
{
    internal class UpdateAppInteractor : IInteractor<UpdateAppCommand>
    {
        private readonly IValidator<UpdateAppCommand> validator;
        private readonly IMapper<UpdateAppCommand, App> mapper;
        private readonly IUpdateGateway<App> repository;
        private readonly IGetByIdGateway<App> getRepository;

        public UpdateAppInteractor(
            IValidator<UpdateAppCommand> validator,
            IMapper<UpdateAppCommand, App> mapper,
            IUpdateGateway<App> repository,
            IGetByIdGateway<App> getRepository)
        {
            this.validator = validator;
            this.mapper = mapper;
            this.repository = repository;
            this.getRepository = getRepository;
        }

        public async Task<Response> ExecuteUseCase(UpdateAppCommand requestModel)
        {
            Response response = validator.Validate(requestModel);
            if (response.IsValid)
            {
                try
                {
                    App entity = getRepository.GetById(requestModel.Id);
                    if (entity == null)
                    {
                        response.AddError(ErrorCodes.NotFound, $"Resource {nameof(App)} with id {requestModel.Id} not found.");
                        return response;
                    }

                    mapper.Map(requestModel, entity);
                    response.SetParameter(entity);

                    int repositoryResult = await repository.Update(entity);
                    if (repositoryResult != 1)
                    {
                        response.AddError(ErrorCodes.InternalServerError, $"Failed to create {nameof(App)}.");
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
