using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Components
{
    internal class DeleteComponentValidator : AbstractValidator<DeleteComponentRequestModel>, IValidator<DeleteComponentRequestModel>
    {
        public DeleteComponentValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(DeleteComponentRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
