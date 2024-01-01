using System;
using System.Diagnostics.CodeAnalysis;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Initializers
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
        [ExcludeFromCodeCoverage]
        public InitializationException()
        {
        }

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
    }
}
