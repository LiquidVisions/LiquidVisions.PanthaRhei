using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.ConnectionStrings;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.ConnectionStrings
{
    internal class DeleteConnectionStringValidator : AbstractValidator<DeleteConnectionStringCommand>, IValidator<DeleteConnectionStringCommand>
    {
        public DeleteConnectionStringValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(DeleteConnectionStringCommand objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
