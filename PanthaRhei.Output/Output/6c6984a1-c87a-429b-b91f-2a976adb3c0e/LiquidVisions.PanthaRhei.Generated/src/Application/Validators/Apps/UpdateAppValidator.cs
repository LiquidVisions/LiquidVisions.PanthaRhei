using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Apps;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Apps
{
    internal class UpdateAppValidator : AbstractValidator<UpdateAppCommand>, IValidator<UpdateAppCommand>
    {
        public UpdateAppValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(UpdateAppCommand objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
