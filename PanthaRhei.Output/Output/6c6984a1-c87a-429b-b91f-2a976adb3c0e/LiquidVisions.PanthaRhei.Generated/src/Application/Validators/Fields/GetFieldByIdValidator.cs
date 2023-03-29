using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Fields;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Fields
{
    internal class GetFieldByIdValidator : AbstractValidator<GetFieldByIdQuery>, IValidator<GetFieldByIdQuery>
    {
        public GetFieldByIdValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetFieldByIdQuery objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
