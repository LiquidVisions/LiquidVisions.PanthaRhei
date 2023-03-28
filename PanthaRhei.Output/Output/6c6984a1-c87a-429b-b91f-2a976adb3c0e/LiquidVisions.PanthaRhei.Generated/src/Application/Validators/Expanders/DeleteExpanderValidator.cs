﻿using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Expanders;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Expanders
{
    internal class DeleteExpanderValidator : AbstractValidator<DeleteExpanderCommand>, IValidator<DeleteExpanderCommand>
    {
        public DeleteExpanderValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(DeleteExpanderCommand objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
