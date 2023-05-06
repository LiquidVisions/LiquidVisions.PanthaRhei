using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.ConnectionStrings;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.ConnectionStrings
{
    internal class DeleteConnectionStringValidator : AbstractValidator<DeleteConnectionStringRequestModel>, IValidator<DeleteConnectionStringRequestModel>
    {
        public DeleteConnectionStringValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(DeleteConnectionStringRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
