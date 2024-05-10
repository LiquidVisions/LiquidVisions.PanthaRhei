using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Domain
{
    /// <summary>
    /// Represents a response from a use case.
    /// </summary>
    public class Response
    {
        private readonly ICollection<Error> errors = [];


        /// <summary>
        /// Gets a value indicating whether the request was valid.
        /// </summary>
        public bool IsValid
            => errors.Count == 0;

        /// <summary>
        /// Gets a collection of errors that occurred during the request.
        /// </summary>
        public IReadOnlyCollection<Error> Errors
            => errors as IReadOnlyCollection<Error>;

        /// <summary>
        /// Adds an error to the response.
        /// </summary>
        /// <param name="code">The <seealso cref="ErrorCode"/> of the error.</param>
        /// <param name="message">The message of the Error</param>
        public void AddError(ErrorCode code, string message)
            => errors.Add(new Error { ErrorCode = code, Message = message });


    }

    /// <summary>
    /// Represents a response from a use case with a parameter.
    /// </summary>
    /// <typeparam name="TParam"></typeparam>
    public sealed class Response<TParam> : Response
    {
        /// <summary>
        /// Gets or sets the parameter of the response.
        /// </summary>
        public TParam Parameter { get; set; }
    }
}
