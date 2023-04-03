using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Fields;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Fields
{
    internal class CreateFieldValidator : AbstractValidator<CreateFieldCommand>, IValidator<CreateFieldCommand>
    {
        public CreateFieldValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(CreateFieldCommand objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
