using System;

namespace LiquidVisions.PanthaRhei.Domain.Logging
{
    /// <summary>
    /// The Logger.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Writes the message at the trace level.
        /// </summary>
        /// <param name="message">The message that needs to be logged.</param>
        void Trace(string message);

        /// <summary>
        /// Writes the diagnostic message at the Trace level using the specified options.
        /// </summary>
        /// <param name="message">A string containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        void Trace(string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message at the Debug level.
        /// </summary>
        /// <param name="message">The log message.</param>
        void Debug(string message);

        /// <summary>
        /// Writes the diagnostic message at the Debug level using the specified options.
        /// </summary>
        /// <param name="message">A string containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        void Debug(string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message at the Info level.
        /// </summary>
        /// <param name="message">The log message.</param>
        void Info(string message);

        /// <summary>
        /// Writes the diagnostic message at the Info level using the specified options.
        /// </summary>
        /// <param name="message">A string containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        void Info(string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message at the Warn level.
        /// </summary>
        /// <param name="message">The diagnostic message.</param>
        void Warn(string message);

        /// <summary>
        /// Writes the diagnostic message at the Warn level using the specified options.
        /// </summary>
        /// <param name="message">A string containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        void Warn(string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message and exception at the Warn level.
        /// </summary>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string to be written.</param>
        void Warn(Exception exception, string message);

        /// <summary>
        /// Writes the diagnostic message and exception at the Warn level.
        /// </summary>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string to be written.</param>
        /// <param name="args">Arguments to format.</param>
        void Warn(Exception exception, string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message at the Fatal level.
        /// </summary>
        /// <param name="message">The log message.</param>
        void Fatal(string message);

        /// <summary>
        /// Writes the diagnostic message at the Fatal level using the specified options.
        /// </summary>
        /// <param name="message">A string containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        void Fatal(string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message and exception at the Fatal level.
        /// </summary>
        /// <param name="exception">A string to be written.</param>
        /// <param name="message">An exception to be logged.</param>
        void Fatal(Exception exception, string message);

        /// <summary>
        /// Writes the diagnostic message and exception at the Fatal level.
        /// </summary>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string to be written.</param>
        /// <param name="args">Arguments to format.</param>
        void Fatal(Exception exception, string message, params object[] args);
    }
}
