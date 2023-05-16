using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Entities
{
    internal class CreateEntityValidator : AbstractValidator<CreateEntityRequestModel>, IValidator<CreateEntityRequestModel>
    {
        public CreateEntityValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(CreateEntityRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
