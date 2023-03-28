using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Relationships;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Relationships
{
    internal class DeleteRelationshipValidator : AbstractValidator<DeleteRelationshipCommand>, IValidator<DeleteRelationshipCommand>
    {
        public DeleteRelationshipValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(DeleteRelationshipCommand objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
