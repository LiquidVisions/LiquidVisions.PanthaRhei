using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.ConnectionStrings;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.ConnectionStrings
{
    internal class CreateConnectionStringValidator : AbstractValidator<CreateConnectionStringCommand>, IValidator<CreateConnectionStringCommand>
    {
        public CreateConnectionStringValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(CreateConnectionStringCommand objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
