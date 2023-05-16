using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Apps
{
    internal class DeleteAppValidator : AbstractValidator<DeleteAppRequestModel>, IValidator<DeleteAppRequestModel>
    {
        public DeleteAppValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(DeleteAppRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
