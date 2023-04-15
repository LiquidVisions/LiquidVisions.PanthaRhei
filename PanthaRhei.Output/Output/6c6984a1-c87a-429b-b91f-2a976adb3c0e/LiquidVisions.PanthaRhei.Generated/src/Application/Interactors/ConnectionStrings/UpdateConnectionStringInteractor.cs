using System;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.ConnectionStrings;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.ConnectionStrings
{
    internal class UpdateConnectionStringInteractor : IInteractor<UpdateConnectionStringRequestModel>
    {
        private readonly IValidator<UpdateConnectionStringRequestModel> validator;
        private readonly IMapper<UpdateConnectionStringRequestModel, ConnectionString> mapper;
        private readonly IUpdateGateway<ConnectionString> repository;
        private readonly IGetByIdGateway<ConnectionString> getRepository;

        public UpdateConnectionStringInteractor(
            IValidator<UpdateConnectionStringRequestModel> validator,
            IMapper<UpdateConnectionStringRequestModel, ConnectionString> mapper,
            IUpdateGateway<ConnectionString> repository,
            IGetByIdGateway<ConnectionString> getRepository)
        {
            this.validator = validator;
            this.mapper = mapper;
            this.repository = repository;
            this.getRepository = getRepository;
        }

        public async Task<Response> ExecuteUseCase(UpdateConnectionStringRequestModel requestModel)
        {
            Response response = validator.Validate(requestModel);
            if (response.IsValid)
            {
                try
                {
                    ConnectionString entity = getRepository.GetById(requestModel.Id);
                    if (entity == null)
                    {
                        response.AddError(ErrorCodes.NotFound, $"Resource {nameof(ConnectionString)} with id {requestModel.Id} not found.");
                        return response;
                    }

                    mapper.Map(requestModel, entity);
                    response.SetParameter(entity);

                    int repositoryResult = await repository.Update(entity);
                    if (repositoryResult != 1)
                    {
                        response.AddError(ErrorCodes.InternalServerError, $"Failed to create {nameof(ConnectionString)}.");
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
