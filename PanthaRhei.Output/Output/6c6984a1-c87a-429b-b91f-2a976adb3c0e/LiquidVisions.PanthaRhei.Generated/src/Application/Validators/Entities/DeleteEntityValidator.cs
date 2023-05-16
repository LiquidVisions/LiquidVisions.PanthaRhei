using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Entities
{
    internal class DeleteEntityValidator : AbstractValidator<DeleteEntityRequestModel>, IValidator<DeleteEntityRequestModel>
    {
        public DeleteEntityValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(DeleteEntityRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
