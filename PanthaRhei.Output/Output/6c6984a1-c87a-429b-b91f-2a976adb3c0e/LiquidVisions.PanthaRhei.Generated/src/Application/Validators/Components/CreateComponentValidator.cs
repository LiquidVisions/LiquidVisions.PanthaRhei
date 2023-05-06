using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Components
{
    internal class CreateComponentValidator : AbstractValidator<CreateComponentRequestModel>, IValidator<CreateComponentRequestModel>
    {
        public CreateComponentValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(CreateComponentRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
