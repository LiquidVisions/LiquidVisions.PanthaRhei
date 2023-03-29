using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Components;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Components
{
    internal class GetComponentByIdValidator : AbstractValidator<GetComponentByIdQuery>, IValidator<GetComponentByIdQuery>
    {
        public GetComponentByIdValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetComponentByIdQuery objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
