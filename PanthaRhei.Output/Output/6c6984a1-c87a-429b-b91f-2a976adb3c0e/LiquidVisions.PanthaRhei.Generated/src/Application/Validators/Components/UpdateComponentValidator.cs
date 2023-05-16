using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Components
{
    internal class UpdateComponentValidator : AbstractValidator<UpdateComponentRequestModel>, IValidator<UpdateComponentRequestModel>
    {
        public UpdateComponentValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(UpdateComponentRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
