using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.Expanders
{
    internal class GetExpanderInteractor : IInteractor<GetExpanderByIdRequestModel>
    {
        private readonly IValidator<GetExpanderByIdRequestModel> validator;
        private readonly IGetByIdGateway<Expander> repository;

        public GetExpanderInteractor(
            IValidator<GetExpanderByIdRequestModel> validator,
            IGetByIdGateway<Expander> repository)
        {
            this.validator = validator;
            this.repository = repository;
        }

        public Task<Response> ExecuteUseCase(GetExpanderByIdRequestModel model)
        {
            return Task.Run(() =>
            {
                Response response = validator
                    .Validate(model);

                if (response.IsValid)
                {
                    try
                    {
                        Expander entity = repository.GetById(model.Id);
                        if (entity == null)
                        {
                            response.AddError(ErrorCodes.NotFound, $"Expander ({model.Id}) not found.");
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
