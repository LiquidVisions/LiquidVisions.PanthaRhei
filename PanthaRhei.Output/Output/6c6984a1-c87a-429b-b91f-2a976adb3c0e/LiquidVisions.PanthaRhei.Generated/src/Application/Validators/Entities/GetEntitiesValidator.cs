using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Entities
{
    internal class GetEntitiesValidator : AbstractValidator<GetEntitiesRequestModel>, IValidator<GetEntitiesRequestModel>
    {
        public GetEntitiesValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetEntitiesRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
