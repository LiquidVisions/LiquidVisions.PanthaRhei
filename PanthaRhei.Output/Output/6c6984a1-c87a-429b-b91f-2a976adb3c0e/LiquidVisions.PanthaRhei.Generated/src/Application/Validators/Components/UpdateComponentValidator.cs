using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Components;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Components
{
    internal class UpdateComponentValidator : AbstractValidator<UpdateComponentCommand>, IValidator<UpdateComponentCommand>
    {
        public UpdateComponentValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(UpdateComponentCommand objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
