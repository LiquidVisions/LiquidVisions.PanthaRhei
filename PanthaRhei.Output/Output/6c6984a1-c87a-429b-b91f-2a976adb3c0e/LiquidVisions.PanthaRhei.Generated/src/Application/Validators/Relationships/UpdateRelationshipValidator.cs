using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Relationships
{
    internal class UpdateRelationshipValidator : AbstractValidator<UpdateRelationshipRequestModel>, IValidator<UpdateRelationshipRequestModel>
    {
        public UpdateRelationshipValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(UpdateRelationshipRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
