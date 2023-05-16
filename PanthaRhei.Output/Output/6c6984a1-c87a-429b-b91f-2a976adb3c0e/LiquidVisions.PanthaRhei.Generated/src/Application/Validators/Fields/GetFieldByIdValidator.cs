using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Fields;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Fields
{
    internal class GetFieldByIdValidator : AbstractValidator<GetFieldByIdRequestModel>, IValidator<GetFieldByIdRequestModel>
    {
        public GetFieldByIdValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetFieldByIdRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
