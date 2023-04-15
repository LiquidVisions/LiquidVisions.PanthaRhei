using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Apps
{
    internal class GetAppByIdValidator : AbstractValidator<GetAppByIdRequestModel>, IValidator<GetAppByIdRequestModel>
    {
        public GetAppByIdValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetAppByIdRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
