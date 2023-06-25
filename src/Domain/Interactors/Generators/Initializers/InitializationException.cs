using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Initializers
{
    /// <summary>
    /// Represents an error while merging Plugin Xml.
    /// </summary>
    [Serializable]
    public class InitializationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InitializationException"/> class.
        /// </summary>
        /// <param name="message">the exception message.</param>
        public InitializationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InitializationException"/> class.
        /// </summary>
        /// <param name="message">the exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public InitializationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InitializationException"/> class.
        /// </summary>
        /// <param name="info"><seealso cref="SerializationInfo"/></param>
        /// <param name="context"><seealso cref="StreamingContext"/></param>
        [ExcludeFromCodeCoverage]
        protected InitializationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
