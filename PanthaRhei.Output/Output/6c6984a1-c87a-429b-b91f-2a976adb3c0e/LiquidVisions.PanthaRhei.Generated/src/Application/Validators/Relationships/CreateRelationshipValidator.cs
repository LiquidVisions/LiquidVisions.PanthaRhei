using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Relationships
{
    internal class CreateRelationshipValidator : AbstractValidator<CreateRelationshipRequestModel>, IValidator<CreateRelationshipRequestModel>
    {
        public CreateRelationshipValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(CreateRelationshipRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
