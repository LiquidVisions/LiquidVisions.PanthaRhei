using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Expanders
{
    internal class GetExpanderByIdValidator : AbstractValidator<GetExpanderByIdQuery>, IValidator<GetExpanderByIdQuery>
    {
        public GetExpanderByIdValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetExpanderByIdQuery objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
