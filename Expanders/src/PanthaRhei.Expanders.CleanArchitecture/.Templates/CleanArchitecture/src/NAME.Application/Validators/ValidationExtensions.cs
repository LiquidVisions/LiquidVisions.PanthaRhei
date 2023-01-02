using FluentValidation.Results;

namespace NS.Application.Validators
{
    internal static class ValidationExtensions
    {
        internal static Response ToResponse(this ValidationResult validationResult)
        {
            Response response = new();

            validationResult
                .Errors
                .ForEach(error =>
                {
                    response.AddError(ErrorCodes.BadRequest, $"{error.ErrorCode} : {error.ErrorMessage}");
                });

            return response;
        }
    }
}
