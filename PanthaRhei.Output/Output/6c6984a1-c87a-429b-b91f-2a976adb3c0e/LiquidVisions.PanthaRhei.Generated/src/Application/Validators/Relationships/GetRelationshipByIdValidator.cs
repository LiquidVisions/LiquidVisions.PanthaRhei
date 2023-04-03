using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Relationships
{
    internal class GetRelationshipByIdValidator : AbstractValidator<GetRelationshipByIdQuery>, IValidator<GetRelationshipByIdQuery>
    {
        public GetRelationshipByIdValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetRelationshipByIdQuery objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
