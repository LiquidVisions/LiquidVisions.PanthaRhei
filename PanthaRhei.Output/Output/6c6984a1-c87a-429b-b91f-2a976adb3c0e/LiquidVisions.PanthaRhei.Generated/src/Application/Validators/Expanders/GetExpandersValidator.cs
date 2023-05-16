using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Expanders
{
    internal class GetExpandersValidator : AbstractValidator<GetExpandersRequestModel>, IValidator<GetExpandersRequestModel>
    {
        public GetExpandersValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetExpandersRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
