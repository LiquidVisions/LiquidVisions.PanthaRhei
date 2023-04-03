using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Expanders
{
    internal class GetExpandersValidator : AbstractValidator<GetExpandersQuery>, IValidator<GetExpandersQuery>
    {
        public GetExpandersValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetExpandersQuery objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
