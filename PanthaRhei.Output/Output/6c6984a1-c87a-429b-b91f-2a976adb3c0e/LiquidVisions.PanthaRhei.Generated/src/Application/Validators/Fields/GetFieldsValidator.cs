using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Fields;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Fields
{
    internal class GetFieldsValidator : AbstractValidator<GetFieldsQuery>, IValidator<GetFieldsQuery>
    {
        public GetFieldsValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetFieldsQuery objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
