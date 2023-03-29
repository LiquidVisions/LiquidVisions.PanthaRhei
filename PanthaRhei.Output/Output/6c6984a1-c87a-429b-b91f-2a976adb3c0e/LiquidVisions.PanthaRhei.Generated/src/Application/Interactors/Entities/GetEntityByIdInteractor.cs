using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Entities;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.Entities
{
    internal class GetEntityInteractor : IInteractor<GetEntityByIdQuery>
    {
        private readonly IValidator<GetEntityByIdQuery> validator;
        private readonly IGetByIdGateway<Entity> repository;

        public GetEntityInteractor(
            IValidator<GetEntityByIdQuery> validator,
            IGetByIdGateway<Entity> repository)
        {
            this.validator = validator;
            this.repository = repository;
        }

        public Task<Response> ExecuteUseCase(GetEntityByIdQuery model)
        {
            return Task.Run(() =>
            {
                Response response = validator
                    .Validate(model);

                if (response.IsValid)
                {
                    try
                    {
                        Entity entity = repository.GetById(model.Id);
                        if (entity == null)
                        {
                            response.AddError(ErrorCodes.NotFound, $"Entity ({model.Id}) not found.");
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
