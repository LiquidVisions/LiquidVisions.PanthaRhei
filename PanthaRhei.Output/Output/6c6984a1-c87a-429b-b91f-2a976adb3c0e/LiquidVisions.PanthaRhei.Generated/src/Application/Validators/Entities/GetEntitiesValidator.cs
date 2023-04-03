using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Entities
{
    internal class GetEntitiesValidator : AbstractValidator<GetEntitiesQuery>, IValidator<GetEntitiesQuery>
    {
        public GetEntitiesValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetEntitiesQuery objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
