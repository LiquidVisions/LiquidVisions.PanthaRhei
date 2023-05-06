using System;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.ConnectionStrings;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.ConnectionStrings
{
    internal class CreateConnectionStringInteractor : IInteractor<CreateConnectionStringRequestModel>
    {
        private readonly IValidator<CreateConnectionStringRequestModel> validator;
        private readonly IMapper<CreateConnectionStringRequestModel, ConnectionString> mapper;
        private readonly ICreateGateway<ConnectionString> repository;

        public CreateConnectionStringInteractor(
            IValidator<CreateConnectionStringRequestModel> validator,
            IMapper<CreateConnectionStringRequestModel, ConnectionString> mapper,
            ICreateGateway<ConnectionString> gateway)
        {
            this.validator = validator;
            this.mapper = mapper;
            repository = gateway;
        }

        public async Task<Response> ExecuteUseCase(CreateConnectionStringRequestModel requestModel)
        {
            Response result = validator.Validate(requestModel);
            if (result.IsValid)
            {
                try
                {
                    ConnectionString entity = mapper.Map(requestModel);
                    entity.Id = Guid.NewGuid();

                    result.SetParameter(entity);

                    int repositoryResult = await repository.Create(entity);
                    if (repositoryResult != 1)
                    {
                        result.AddError(ErrorCodes.InternalServerError, $"Failed to create {nameof(ConnectionString)}.");
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
