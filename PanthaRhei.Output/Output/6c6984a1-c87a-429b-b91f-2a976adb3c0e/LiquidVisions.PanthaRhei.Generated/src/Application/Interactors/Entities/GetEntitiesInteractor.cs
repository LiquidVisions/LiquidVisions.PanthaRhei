using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.Entities
{
    internal class GetEntitiesInteractor : IInteractor<GetEntitiesQuery>
    {
        private readonly IValidator<GetEntitiesQuery> validator;
        private readonly IGetGateway<Entity> repository;

        public GetEntitiesInteractor(
            IValidator<GetEntitiesQuery> validator,
            IGetGateway<Entity> repository)
        {
            this.validator = validator;
            this.repository = repository;
        }

        public Task<Response> ExecuteUseCase(GetEntitiesQuery model)
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
