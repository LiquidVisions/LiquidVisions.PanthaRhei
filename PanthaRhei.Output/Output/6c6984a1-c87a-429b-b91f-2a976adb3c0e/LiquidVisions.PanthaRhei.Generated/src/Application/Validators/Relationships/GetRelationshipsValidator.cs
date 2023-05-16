using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Relationships
{
    internal class GetRelationshipsValidator : AbstractValidator<GetRelationshipsRequestModel>, IValidator<GetRelationshipsRequestModel>
    {
        public GetRelationshipsValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetRelationshipsRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
