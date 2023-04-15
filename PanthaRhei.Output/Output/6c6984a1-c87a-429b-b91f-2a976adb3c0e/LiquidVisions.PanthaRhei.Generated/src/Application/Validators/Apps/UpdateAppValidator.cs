using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Apps
{
    internal class UpdateAppValidator : AbstractValidator<UpdateAppRequestModel>, IValidator<UpdateAppRequestModel>
    {
        public UpdateAppValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(UpdateAppRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
