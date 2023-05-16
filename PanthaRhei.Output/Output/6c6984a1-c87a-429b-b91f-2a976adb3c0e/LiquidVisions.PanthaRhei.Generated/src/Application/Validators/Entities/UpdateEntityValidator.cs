﻿using FluentValidation;
using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Validators.Entities
{
    internal class UpdateEntityValidator : AbstractValidator<UpdateEntityRequestModel>, IValidator<UpdateEntityRequestModel>
    {
        public UpdateEntityValidator()
        {
            #region ns-custom-validations
            #endregion ns-custom-validations
        }

        public new Response Validate(UpdateEntityRequestModel objectToValidate) => 
            base.Validate(objectToValidate)
                .ToResponse();
    }
}
