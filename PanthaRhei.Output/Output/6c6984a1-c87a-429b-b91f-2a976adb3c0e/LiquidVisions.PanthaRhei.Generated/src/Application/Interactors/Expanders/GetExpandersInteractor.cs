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
    internal class GetExpandersInteractor : IInteractor<GetExpandersRequestModel>
    {
        private readonly IValidator<GetExpandersRequestModel> validator;
        private readonly IGetGateway<Expander> repository;

        public GetExpandersInteractor(
            IValidator<GetExpandersRequestModel> validator,
            IGetGateway<Expander> repository)
        {
            this.validator = validator;
            this.repository = repository;
        }

        public Task<Response> ExecuteUseCase(GetExpandersRequestModel model)
        {
            return Task.Run(() =>
            {
                Response response = validator
                    .Validate(model);

                if (response.IsValid)
                {
                    #region ns-custom-query
                    var queryResult = repository
                        .Get()
                        .ToList();
                    #endregion ns-custom-query
                    response.SetParameter(queryResult);
                }

                return response;
            });
        }
    }
}
