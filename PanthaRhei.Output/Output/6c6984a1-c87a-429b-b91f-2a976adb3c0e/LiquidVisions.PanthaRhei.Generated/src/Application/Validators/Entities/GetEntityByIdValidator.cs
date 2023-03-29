using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Entities
{
    internal class GetEntityByIdValidator : AbstractValidator<GetEntityByIdQuery>, IValidator<GetEntityByIdQuery>
    {
        public GetEntityByIdValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetEntityByIdQuery objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
