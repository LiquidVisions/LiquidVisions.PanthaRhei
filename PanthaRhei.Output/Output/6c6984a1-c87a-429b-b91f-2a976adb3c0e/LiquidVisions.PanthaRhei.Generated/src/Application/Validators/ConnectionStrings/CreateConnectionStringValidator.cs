using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.ConnectionStrings;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.ConnectionStrings
{
    internal class CreateConnectionStringValidator : AbstractValidator<CreateConnectionStringRequestModel>, IValidator<CreateConnectionStringRequestModel>
    {
        public CreateConnectionStringValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(CreateConnectionStringRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
