using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Expanders
{
    internal class CreateExpanderValidator : AbstractValidator<CreateExpanderRequestModel>, IValidator<CreateExpanderRequestModel>
    {
        public CreateExpanderValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(CreateExpanderRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
