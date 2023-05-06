using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Relationships
{
    internal class DeleteRelationshipValidator : AbstractValidator<DeleteRelationshipRequestModel>, IValidator<DeleteRelationshipRequestModel>
    {
        public DeleteRelationshipValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(DeleteRelationshipRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
