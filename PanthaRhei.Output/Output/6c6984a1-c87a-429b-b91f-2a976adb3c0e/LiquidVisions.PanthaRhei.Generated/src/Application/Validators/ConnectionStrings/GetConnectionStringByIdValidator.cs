using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.ConnectionStrings;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.ConnectionStrings
{
    internal class GetConnectionStringByIdValidator : AbstractValidator<GetConnectionStringByIdRequestModel>, IValidator<GetConnectionStringByIdRequestModel>
    {
        public GetConnectionStringByIdValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetConnectionStringByIdRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
