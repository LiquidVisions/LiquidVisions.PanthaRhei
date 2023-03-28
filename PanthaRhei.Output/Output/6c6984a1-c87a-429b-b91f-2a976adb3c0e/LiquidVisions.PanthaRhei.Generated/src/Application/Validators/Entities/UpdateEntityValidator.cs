using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Entities
{
    internal class UpdateEntityValidator : AbstractValidator<UpdateEntityCommand>, IValidator<UpdateEntityCommand>
    {
        public UpdateEntityValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(UpdateEntityCommand objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
