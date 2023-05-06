using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Expanders
{
    internal class DeleteExpanderValidator : AbstractValidator<DeleteExpanderRequestModel>, IValidator<DeleteExpanderRequestModel>
    {
        public DeleteExpanderValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(DeleteExpanderRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
