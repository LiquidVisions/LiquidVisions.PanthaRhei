using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Templates
{
    /// <summary>
    /// Generic Exception for template rendering.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class TemplateException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateException"/> class.
        /// </summary>
        public TemplateException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateException"/> class.
        /// </summary>
        /// <param name="message">The Message of the exception.</param>
        public TemplateException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateException"/> class.
        /// </summary>
        /// <param name="message">The Message Of the Exception.</param>
        /// <param name="innerException">The Inner exception.</param>
        public TemplateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
