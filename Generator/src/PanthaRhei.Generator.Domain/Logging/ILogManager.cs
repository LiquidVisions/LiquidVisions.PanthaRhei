namespace LiquidVisions.PanthaRhei.Generator.Domain.Logging
{
    /// <summary>
    /// represents a generic logmanager.
    /// </summary>
    public interface ILogManager
    {
        /// <summary>
        /// Gets an instance of <see cref="ILogger"/>.
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// Gets an instance of the logger for authentication.
        /// </summary>
        /// <returns>The authentication logger.</returns>
        ILogger GetAuthenticationLogger();

        /// <summary>
        /// Gets an instance of the logger for exceptions at the application level.
        /// </summary>
        /// <returns>The exception logger.</returns>
        ILogger GetExceptionLogger();

        /// <summary>
        /// Gets an instance of the logger for the specified loggername.
        /// </summary>
        /// <param name="loggerName">The name of the logger.</param>
        /// <returns>The logger for the specified loggername.</returns>
        ILogger Get(string loggerName);
    }
}
