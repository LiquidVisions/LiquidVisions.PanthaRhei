using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework
{
    /// <summary>
    /// object that contains extension methods for <seealso cref="Context"/>.
    /// </summary>
    internal static class ContextExtensions
    {
        /// <summary>
        /// Get a fully configured <seealso cref="ILoggerFactory"/>.
        /// </summary>
        /// <returns><seealso cref="ILoggerFactory"/></returns>
        [ExcludeFromCodeCoverage]
        internal static ILoggerFactory GetLoggerFactory()
        {
            return LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
        }
    }
}