﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Generators
{
    /// <summary>
    /// Represents an error while merging Plugin Xml.
    /// </summary>
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class CodeGenerationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGenerationException"/> class.
        /// </summary>
        public CodeGenerationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGenerationException"/> class.
        /// </summary>
        /// <param name="message">the exception message.</param>
        public CodeGenerationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGenerationException"/> class.
        /// </summary>
        /// <param name="message">the exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public CodeGenerationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
