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
        private readonly LogManager logManager = new("C:\\Some\\Root\\Path");

        /// <summary>
        /// Test for <see cref="LogManager.Logger"/>.
        /// </summary>
        [Fact]
        public void LoggerShouldBeOfCorrectType()
        {
            // arrange
            // act
            ILogger logger = logManager.Logger;

            // assert
            Assert.NotNull(logger);
            Assert.Equal(typeof(Logger), logger.GetType());
        }

        /// <summary>
        /// Test for <see cref="LogManager.Logger"/>.
        /// Should be the same instance.
        /// </summary>
        [Fact]
        public void LoggerShouldBeSameInstance()
        {
            // arrange
            // act
            ILogger logger1 = logManager.Logger;
            ILogger logger2 = logManager.Logger;

            // assert
            Assert.NotNull(logger1);
            Assert.NotNull(logger2);
            Assert.Same(logger1, logger2);
        }

        /// <summary>
        /// Test for <see cref="LogManager.GetLoggerByName(string)"/>.
        /// Should be the same instance of type <seealso cref="Loggers.DefaultLogger"/>.
        /// </summary>
        [Fact]
        public void DefaultLoggerShouldBeSameInstance()
        {
            // arrange
            // act
            ILogger logger1 = logManager.Logger;
            ILogger logger2 = logManager.GetLoggerByName(Loggers.DefaultLogger);

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
        public void AuthenticationLoggerShouldBeSameInstance()
        {
            // arrange
            // act
            ILogger logger1 = logManager.GetAuthenticationLogger();
            ILogger logger2 = logManager.GetLoggerByName(Loggers.AuthenticationLogger);

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
        public void ExceptionLoggerShouldBeSameInstance()
        {
            // arrange
            // act
            ILogger logger1 = logManager.GetExceptionLogger();
            ILogger logger2 = logManager.GetLoggerByName(Loggers.ExceptionLogger);

            // assert
            Assert.NotNull(logger1);
            Assert.NotNull(logger2);
            Assert.Same(logger1, logger2);
        }

        /// <summary>
        /// Test for <see cref="LogManager.GetLoggerByName(string)"/>.
        /// Should throw exception when logger name is invalid.
        /// </summary>
        [Fact]
        public void InvalidLoggerNameShouldThrowException()
        {
            // arrange
            string loggerName = "InvalidName";

            // act
            void Action() => logManager.GetLoggerByName(loggerName);

            // assert
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => Action());
            Assert.Equal($"Specified argument was out of the range of valid values. (Parameter 'loggerName')", ex.Message);
        }

        /// <summary>
        /// Test for <see cref="LogManager.GetLoggerByName(string)"/>.
        /// Should throw exception when logger name is null.
        /// </summary>
        [Fact]
        public void LoggerNameNullShouldThrowException()
        {
            // arrange

            // act
            void Action() => logManager.GetLoggerByName(string.Empty);

            // assert
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => Action());
            Assert.Equal($"Value cannot be null. (Parameter 'loggerName')", ex.Message);
        }
    }
}
