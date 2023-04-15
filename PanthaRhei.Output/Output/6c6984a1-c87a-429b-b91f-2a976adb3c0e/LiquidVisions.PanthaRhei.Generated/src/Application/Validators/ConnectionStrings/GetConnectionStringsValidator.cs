using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.ConnectionStrings;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.ConnectionStrings
{
    internal class GetConnectionStringsValidator : AbstractValidator<GetConnectionStringsRequestModel>, IValidator<GetConnectionStringsRequestModel>
    {
        public GetConnectionStringsValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetConnectionStringsRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
