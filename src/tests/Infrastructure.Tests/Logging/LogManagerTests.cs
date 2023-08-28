using System;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Infrastructure.Logging;
using Xunit;

namespace LiquidVisions.PanthaRhei.Infrastructure.Tests.Logging
{
    /// <summary>
    /// Tests for <see cref="LogManager"/>.
    /// </summary>
    public class LogManagerTests
    {
        private readonly LogManager _logManager = new(new GenerationOptions { Root = "C:\\Some\\Root\\Path" });

        /// <summary>
        /// Test for <see cref="LogManager.Logger"/>.
        /// </summary>
        [Fact]
        public void Logger_ShouldBeOfCorrectType()
        {
            // arrange
            // act
            ILogger logger = _logManager.Logger;

            // assert
            Assert.NotNull(logger);
            Assert.Equal(typeof(Logger), logger.GetType());
        }

        /// <summary>
        /// Test for <see cref="LogManager.Logger"/>.
        /// Should be the same instance.
        /// </summary>
        [Fact]
        public void Logger_ShouldBeSameInstance()
        {
            // arrange
            // act
            ILogger logger1 = _logManager.Logger;
            ILogger logger2 = _logManager.Logger;

            // assert
            Assert.NotNull(logger1);
            Assert.NotNull(logger2);
            Assert.Same(logger1, logger2);
        }

        /// <summary>
        /// Test for <see cref="LogManager.Get(string)"/>.
        /// Should be the same instance of type <seealso cref="Loggers.DefaultLogger"/>.
        /// </summary>
        [Fact]
        public void DefaultLogger_ShouldBeSameInstance()
        {
            // arrange
            // act
            ILogger logger1 = _logManager.Logger;
            ILogger logger2 = _logManager.Get(Loggers.DefaultLogger);

            // assert
            Assert.NotNull(logger1);
            Assert.NotNull(logger2);
            Assert.Same(logger1, logger2);
        }

        /// <summary>
        /// Test for <see cref="LogManager.GetAuthenticationLogger()"/>.
        /// Should be the same instance of type <seealso cref="Loggers.AuthenticationLogger"/>.
        /// </summary>
        [Fact]
        public void AuthenticationLogger_ShouldBeSameInstance()
        {
            // arrange
            // act
            ILogger logger1 = _logManager.GetAuthenticationLogger();
            ILogger logger2 = _logManager.Get(Loggers.AuthenticationLogger);

            // assert
            Assert.NotNull(logger1);
            Assert.NotNull(logger2);
            Assert.Same(logger1, logger2);
        }

        /// <summary>
        /// Test for <see cref="LogManager.GetExceptionLogger()"/>.
        /// Should be the same instance of type <seealso cref="Loggers.ExceptionLogger"/>.
        /// </summary>
        [Fact]
        public void ExceptionLogger_ShouldBeSameInstance()
        {
            // arrange
            // act
            ILogger logger1 = _logManager.GetExceptionLogger();
            ILogger logger2 = _logManager.Get(Loggers.ExceptionLogger);

            // assert
            Assert.NotNull(logger1);
            Assert.NotNull(logger2);
            Assert.Same(logger1, logger2);
        }

        /// <summary>
        /// Test for <see cref="LogManager.Get(string)"/>.
        /// Should throw exception when logger name is invalid.
        /// </summary>
        [Fact]
        public void InvalidLoggerName_ShouldThrowException()
        {
            // arrange
            string loggerName = "InvalidName";

            // act
            void Action() => _logManager.Get(loggerName);

            // assert
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => Action());
            Assert.Equal($"Specified argument was out of the range of valid values. (Parameter 'loggerName')", ex.Message);
        }

        /// <summary>
        /// Test for <see cref="LogManager.Get(string)"/>.
        /// Should throw exception when logger name is null.
        /// </summary>
        [Fact]
        public void LoggerNameNull_ShouldThrowException()
        {
            // arrange

            // act
            void Action() => _logManager.Get(string.Empty);

            // assert
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => Action());
            Assert.Equal($"Value cannot be null. (Parameter 'loggerName')", ex.Message);
        }
    }
}
