using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.ConnectionStrings;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.ConnectionStrings
{
    internal class GetConnectionStringsValidator : AbstractValidator<GetConnectionStringsQuery>, IValidator<GetConnectionStringsQuery>
    {
        public GetConnectionStringsValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetConnectionStringsQuery objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
