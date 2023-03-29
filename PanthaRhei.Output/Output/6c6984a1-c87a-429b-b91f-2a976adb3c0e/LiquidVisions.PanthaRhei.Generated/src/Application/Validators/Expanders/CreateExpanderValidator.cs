using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Expanders;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Expanders
{
    internal class CreateExpanderValidator : AbstractValidator<CreateExpanderCommand>, IValidator<CreateExpanderCommand>
    {
        public CreateExpanderValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(CreateExpanderCommand objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
