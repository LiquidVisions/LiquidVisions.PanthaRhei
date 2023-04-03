using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Components
{
    internal class CreateComponentValidator : AbstractValidator<CreateComponentCommand>, IValidator<CreateComponentCommand>
    {
        public CreateComponentValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(CreateComponentCommand objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
