using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.ConnectionStrings;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.ConnectionStrings
{
    internal class UpdateConnectionStringValidator : AbstractValidator<UpdateConnectionStringRequestModel>, IValidator<UpdateConnectionStringRequestModel>
    {
        public UpdateConnectionStringValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(UpdateConnectionStringRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
