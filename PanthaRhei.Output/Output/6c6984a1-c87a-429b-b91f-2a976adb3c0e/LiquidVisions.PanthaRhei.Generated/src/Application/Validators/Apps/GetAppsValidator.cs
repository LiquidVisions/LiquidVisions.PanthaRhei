using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Apps;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Apps
{
    internal class GetAppsValidator : AbstractValidator<GetAppsQuery>, IValidator<GetAppsQuery>
    {
        public GetAppsValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetAppsQuery objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
