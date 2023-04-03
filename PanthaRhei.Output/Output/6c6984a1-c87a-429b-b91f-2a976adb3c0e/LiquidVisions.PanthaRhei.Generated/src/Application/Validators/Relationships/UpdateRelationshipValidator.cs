using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Relationships
{
    internal class UpdateRelationshipValidator : AbstractValidator<UpdateRelationshipCommand>, IValidator<UpdateRelationshipCommand>
    {
        public UpdateRelationshipValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(UpdateRelationshipCommand objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
