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
    internal class GetConnectionStringsInteractor : IInteractor<GetConnectionStringsRequestModel>
    {
        private readonly IValidator<GetConnectionStringsRequestModel> validator;
        private readonly IGetGateway<ConnectionString> repository;

        public GetConnectionStringsInteractor(
            IValidator<GetConnectionStringsRequestModel> validator,
            IGetGateway<ConnectionString> repository)
        {
            this.validator = validator;
            this.repository = repository;
        }

        public Task<Response> ExecuteUseCase(GetConnectionStringsRequestModel model)
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
