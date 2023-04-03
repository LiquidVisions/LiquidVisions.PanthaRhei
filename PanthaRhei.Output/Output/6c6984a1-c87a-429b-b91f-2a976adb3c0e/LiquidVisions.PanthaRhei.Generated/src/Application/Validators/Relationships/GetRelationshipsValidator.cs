using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Relationships
{
    internal class GetRelationshipsValidator : AbstractValidator<GetRelationshipsQuery>, IValidator<GetRelationshipsQuery>
    {
        public GetRelationshipsValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetRelationshipsQuery objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
