using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Apps
{
    internal class CreateAppValidator : AbstractValidator<CreateAppCommand>, IValidator<CreateAppCommand>
    {
        public CreateAppValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(CreateAppCommand objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
