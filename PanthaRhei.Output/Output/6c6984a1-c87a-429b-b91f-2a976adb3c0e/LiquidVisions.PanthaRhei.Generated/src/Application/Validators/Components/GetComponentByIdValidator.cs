using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Components
{
    internal class GetComponentByIdValidator : AbstractValidator<GetComponentByIdRequestModel>, IValidator<GetComponentByIdRequestModel>
    {
        public GetComponentByIdValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetComponentByIdRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
