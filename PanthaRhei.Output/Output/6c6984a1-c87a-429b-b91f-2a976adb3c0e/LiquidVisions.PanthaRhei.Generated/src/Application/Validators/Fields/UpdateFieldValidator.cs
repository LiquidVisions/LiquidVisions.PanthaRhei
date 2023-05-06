using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Fields;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Fields
{
    internal class UpdateFieldValidator : AbstractValidator<UpdateFieldRequestModel>, IValidator<UpdateFieldRequestModel>
    {
        public UpdateFieldValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(UpdateFieldRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
