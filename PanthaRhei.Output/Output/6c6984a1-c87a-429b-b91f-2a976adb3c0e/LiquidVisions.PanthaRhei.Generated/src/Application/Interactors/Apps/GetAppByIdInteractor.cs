using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Apps;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.Apps
{
    internal class GetAppInteractor : IInteractor<GetAppByIdQuery>
    {
        private readonly IValidator<GetAppByIdQuery> validator;
        private readonly IGetByIdGateway<App> repository;

        public GetAppInteractor(
            IValidator<GetAppByIdQuery> validator,
            IGetByIdGateway<App> repository)
        {
            this.validator = validator;
            this.repository = repository;
        }

        public Task<Response> ExecuteUseCase(GetAppByIdQuery model)
        {
            return Task.Run(() =>
            {
                Response response = validator
                    .Validate(model);

                if (response.IsValid)
                {
                    try
                    {
                        App entity = repository.GetById(model.Id);
                        if (entity == null)
                        {
                            response.AddError(ErrorCodes.NotFound, $"App ({model.Id}) not found.");
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
