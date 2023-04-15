using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Apps
{
    internal class GetAppsValidator : AbstractValidator<GetAppsRequestModel>, IValidator<GetAppsRequestModel>
    {
        public GetAppsValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetAppsRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
