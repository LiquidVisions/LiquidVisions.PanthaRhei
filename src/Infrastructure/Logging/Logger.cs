﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace LiquidVisions.PanthaRhei.Infrastructure.Logging
{
    /// <summary>
    /// Wrapper for the NLog logger.
    /// </summary>
    /// <seealso cref="ILogger" />
    [ExcludeFromCodeCoverage]
    internal class Logger : ILogger
    {
        private readonly NLog.Logger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="name">The name of the logger.</param>
        internal Logger(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            logger = NLog.LogManager.GetLogger(name);
        }

        internal Logger(string name, string root)
            : this(name)
        {
            string logPath = Path.Combine(root, "Logs");
            if (NLog.LogManager.Configuration?.FindTargetByName("file") is AsyncTargetWrapper { WrappedTarget: FileTarget target })
            {
                target.FileName = Path.Combine(logPath, "current.log");
                target.ArchiveFileName = Path.Combine(logPath, DateTime.Now.ToShortDateString(), "archived{###}.log");
            }
        }

        /// <summary>
        /// Writes the message at the trace level.
        /// </summary>
        /// <param name="message">The message that needs to be logged.</param>
        public void Trace(string message)
        {
            logger.Trace(message);
        }

        /// <summary>
        /// Writes the diagnostic message at the Trace level using the specified options.
        /// </summary>
        /// <param name="message">A string containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        public void Trace(string message, params object[] args)
        {
            logger.Trace(CultureInfo.InvariantCulture, message, args);
        }

        /// <summary>
        /// Writes the diagnostic message at the Debug level.
        /// </summary>
        /// <param name="message">The log message.</param>
        public void Debug(string message)
        {
            logger.Debug(message);
        }

        /// <summary>
        /// Writes the diagnostic message at the Debug level using the specified options.
        /// </summary>
        /// <param name="message">A string containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        public void Debug(string message, params object[] args)
        {
            logger.Debug(CultureInfo.InvariantCulture, message, args);
        }

        /// <summary>
        /// Writes the diagnostic message at the Info level.
        /// </summary>
        /// <param name="message">The log message.</param>
        public void Info(string message)
        {
            logger.Info(message);
        }

        /// <summary>
        /// Writes the diagnostic message at the Info level using the specified options.
        /// </summary>
        /// <param name="message">A string containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        public void Info(string message, params object[] args)
        {
            logger.Info(CultureInfo.InvariantCulture, message, args);
        }

        /// <summary>
        /// Writes the diagnostic message at the Warn level.
        /// </summary>
        /// <param name="message">The diagnostic message.</param>
        public void Warn(string message)
        {
            logger.Warn(message);
        }

        /// <summary>
        /// Writes the diagnostic message at the Warn level using the specified options.
        /// </summary>
        /// <param name="message">A string containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        public void Warn(string message, params object[] args)
        {
            logger.Warn(CultureInfo.InvariantCulture, message, args);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Warn level.
        /// </summary>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string to be written.</param>
        public void Warn(Exception exception, string message)
        {
            logger.Warn(exception, CultureInfo.InvariantCulture, message);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Warn level.
        /// </summary>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string to be written.</param>
        /// <param name="args">Arguments to format.</param>
        public void Warn(Exception exception, string message, params object[] args)
        {
            logger.Warn(exception, CultureInfo.InvariantCulture, message, args);
        }

        /// <summary>
        /// Writes the diagnostic message at the Fatal level.
        /// </summary>
        /// <param name="message">The log message.</param>
        public void Fatal(string message)
        {
            logger.Fatal(message);
        }

        /// <summary>
        /// Writes the diagnostic message at the Fatal level using the specified options.
        /// </summary>
        /// <param name="message">A string containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        public void Fatal(string message, params object[] args)
        {
            logger.Fatal(CultureInfo.InvariantCulture, message, args);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Fatal level.
        /// </summary>
        /// <param name="exception">A string to be written.</param>
        /// <param name="message">An exception to be logged.</param>
        public void Fatal(Exception exception, string message)
        {
            logger.Fatal(exception, CultureInfo.InvariantCulture, message);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Fatal level.
        /// </summary>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string to be written.</param>
        /// <param name="args">Arguments to format.</param>
        public void Fatal(Exception exception, string message, params object[] args)
        {
            logger.Fatal(exception, message, args);
        }
    }
}
