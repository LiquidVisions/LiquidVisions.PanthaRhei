using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Fields;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Fields
{
    internal class CreateFieldValidator : AbstractValidator<CreateFieldRequestModel>, IValidator<CreateFieldRequestModel>
    {
        public CreateFieldValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(CreateFieldRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
