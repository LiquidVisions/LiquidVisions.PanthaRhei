using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.ConnectionStrings;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.ConnectionStrings
{
    internal class GetConnectionStringInteractor : IInteractor<GetConnectionStringByIdRequestModel>
    {
        private readonly IValidator<GetConnectionStringByIdRequestModel> validator;
        private readonly IGetByIdGateway<ConnectionString> repository;

        public GetConnectionStringInteractor(
            IValidator<GetConnectionStringByIdRequestModel> validator,
            IGetByIdGateway<ConnectionString> repository)
        {
            this.validator = validator;
            this.repository = repository;
        }

        public Task<Response> ExecuteUseCase(GetConnectionStringByIdRequestModel model)
        {
            return Task.Run(() =>
            {
                Response response = validator
                    .Validate(model);

                if (response.IsValid)
                {
                    try
                    {
                        ConnectionString entity = repository.GetById(model.Id);
                        if (entity == null)
                        {
                            response.AddError(ErrorCodes.NotFound, $"ConnectionString ({model.Id}) not found.");
                            return response;
                        }

                        response.SetParameter(entity);
                    }
                    catch (Exception exception)
                    {
                        response.AddError(ErrorCodes.InternalServerError, exception.Message);
                    }
                }

                return response;
            });
        }
    }
}
