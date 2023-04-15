using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Fields;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Fields
{
    internal class DeleteFieldValidator : AbstractValidator<DeleteFieldRequestModel>, IValidator<DeleteFieldRequestModel>
    {
        public DeleteFieldValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(DeleteFieldRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
