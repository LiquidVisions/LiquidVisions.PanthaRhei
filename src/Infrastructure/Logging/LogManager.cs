using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Infrastructure.Logging
{
    /// <summary>
    /// Wrapper for the NLog LogManager Class.
    /// </summary>
    /// <seealso cref="ILogManager" />
    internal class LogManager(GenerationOptions options) : ILogManager
    {
        private static readonly ConcurrentDictionary<string, ILogger> loggerCache = new();
        private static readonly IEnumerable<string> loggerNames = [Loggers.DefaultLogger, Loggers.ExceptionLogger, Loggers.AuthenticationLogger];

        /// <summary>
        /// Gets an instance of the default logger.
        /// </summary>
        /// <returns>The default logger.</returns>
        public ILogger Logger => GetLoggerByName(Loggers.DefaultLogger);

        /// <summary>
        /// Gets an instance of the logger for authentication.
        /// </summary>
        /// <returns>The authentication logger.</returns>
        public ILogger GetAuthenticationLogger()
        {
            return GetLoggerByName(Loggers.AuthenticationLogger);
        }

        /// <summary>
        /// Gets an instance of the logger for exceptions at the application level.
        /// </summary>
        /// <returns>The exception logger.</returns>
        public ILogger GetExceptionLogger()
        {
            return GetLoggerByName(Loggers.ExceptionLogger);
        }

        /// <summary>
        /// Gets an instance of the logger for the given name.
        /// </summary>
        /// <param name="loggerName">The name of the logger.</param>
        /// <returns>The logger for this name.</returns>
        public ILogger GetLoggerByName(string loggerName)
        {
            if (string.IsNullOrWhiteSpace(loggerName))
            {
                throw new ArgumentNullException(nameof(loggerName));
            }

            if (!loggerNames.Contains(loggerName))
            {
                throw new ArgumentOutOfRangeException(nameof(loggerName));
            }

            return options == null
                ? loggerCache.GetOrAdd(loggerName, new Logger(loggerName))
                : loggerCache.GetOrAdd(loggerName, new Logger(loggerName, options.Root));
        }
    }
}
