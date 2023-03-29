using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Fields;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Fields
{
    internal class UpdateFieldValidator : AbstractValidator<UpdateFieldCommand>, IValidator<UpdateFieldCommand>
    {
        public UpdateFieldValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(UpdateFieldCommand objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
