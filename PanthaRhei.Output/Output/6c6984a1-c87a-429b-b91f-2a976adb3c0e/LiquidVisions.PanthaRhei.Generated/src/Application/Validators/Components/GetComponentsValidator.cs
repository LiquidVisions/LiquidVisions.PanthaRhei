using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Components;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Components
{
    internal class GetComponentsValidator : AbstractValidator<GetComponentsQuery>, IValidator<GetComponentsQuery>
    {
        public GetComponentsValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetComponentsQuery objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
