using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.ConnectionStrings;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.ConnectionStrings
{
    internal class UpdateConnectionStringValidator : AbstractValidator<UpdateConnectionStringCommand>, IValidator<UpdateConnectionStringCommand>
    {
        public UpdateConnectionStringValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(UpdateConnectionStringCommand objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
