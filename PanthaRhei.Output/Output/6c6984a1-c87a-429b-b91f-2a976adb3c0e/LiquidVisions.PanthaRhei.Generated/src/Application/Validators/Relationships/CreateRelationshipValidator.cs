using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Relationships;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Relationships
{
    internal class CreateRelationshipValidator : AbstractValidator<CreateRelationshipCommand>, IValidator<CreateRelationshipCommand>
    {
        public CreateRelationshipValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(CreateRelationshipCommand objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
