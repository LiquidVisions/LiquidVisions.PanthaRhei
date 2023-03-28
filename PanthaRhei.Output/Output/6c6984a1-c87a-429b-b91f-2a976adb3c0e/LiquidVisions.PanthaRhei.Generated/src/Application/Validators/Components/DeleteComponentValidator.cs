using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Components;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Components
{
    internal class DeleteComponentValidator : AbstractValidator<DeleteComponentCommand>, IValidator<DeleteComponentCommand>
    {
        public DeleteComponentValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(DeleteComponentCommand objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
