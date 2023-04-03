using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Entities
{
    internal class CreateEntityValidator : AbstractValidator<CreateEntityCommand>, IValidator<CreateEntityCommand>
    {
        public CreateEntityValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(CreateEntityCommand objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
