using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Fields;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.Fields
{
    internal class GetFieldInteractor : IInteractor<GetFieldByIdQuery>
    {
        private readonly IValidator<GetFieldByIdQuery> validator;
        private readonly IGetByIdGateway<Field> repository;

        public GetFieldInteractor(
            IValidator<GetFieldByIdQuery> validator,
            IGetByIdGateway<Field> repository)
        {
            this.validator = validator;
            this.repository = repository;
        }

        public Task<Response> ExecuteUseCase(GetFieldByIdQuery model)
        {
            return Task.Run(() =>
            {
                Response response = validator
                    .Validate(model);

                if (response.IsValid)
                {
                    try
                    {
                        Field entity = repository.GetById(model.Id);
                        if (entity == null)
                        {
                            response.AddError(ErrorCodes.NotFound, $"Field ({model.Id}) not found.");
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
