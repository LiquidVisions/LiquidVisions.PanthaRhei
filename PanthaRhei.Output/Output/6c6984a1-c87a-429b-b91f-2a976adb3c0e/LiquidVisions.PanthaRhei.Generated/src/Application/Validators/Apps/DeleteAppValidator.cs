using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Apps
{
    internal class DeleteAppValidator : AbstractValidator<DeleteAppCommand>, IValidator<DeleteAppCommand>
    {
        public DeleteAppValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(DeleteAppCommand objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
