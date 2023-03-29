using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Fields;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Fields
{
    internal class DeleteFieldValidator : AbstractValidator<DeleteFieldCommand>, IValidator<DeleteFieldCommand>
    {
        public DeleteFieldValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(DeleteFieldCommand objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
