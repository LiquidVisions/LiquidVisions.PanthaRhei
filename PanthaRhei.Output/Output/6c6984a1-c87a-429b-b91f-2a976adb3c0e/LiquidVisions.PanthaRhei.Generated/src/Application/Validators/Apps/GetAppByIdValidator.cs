using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Apps;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Apps
{
    internal class GetAppByIdValidator : AbstractValidator<GetAppByIdQuery>, IValidator<GetAppByIdQuery>
    {
        public GetAppByIdValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetAppByIdQuery objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
