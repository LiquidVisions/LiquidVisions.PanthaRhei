using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Expanders
{
    internal class UpdateExpanderValidator : AbstractValidator<UpdateExpanderCommand>, IValidator<UpdateExpanderCommand>
    {
        public UpdateExpanderValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(UpdateExpanderCommand objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
