using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Entities
{
    internal class GetEntityByIdValidator : AbstractValidator<GetEntityByIdRequestModel>, IValidator<GetEntityByIdRequestModel>
    {
        public GetEntityByIdValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(GetEntityByIdRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
