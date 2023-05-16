using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Components
{
    internal class GetComponentsValidator : AbstractValidator<GetComponentsRequestModel>, IValidator<GetComponentsRequestModel>
    {
        public GetComponentsValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetComponentsRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
