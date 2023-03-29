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
    internal class GetFieldsInteractor : IInteractor<GetFieldsQuery>
    {
        private readonly IValidator<GetFieldsQuery> validator;
        private readonly IGetGateway<Field> repository;

        public GetFieldsInteractor(
            IValidator<GetFieldsQuery> validator,
            IGetGateway<Field> repository)
        {
            this.validator = validator;
            this.repository = repository;
        }

        public Task<Response> ExecuteUseCase(GetFieldsQuery model)
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
