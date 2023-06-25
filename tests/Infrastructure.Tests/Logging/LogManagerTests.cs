using System;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Infrastructure.Logging;
using Xunit;

namespace LiquidVisions.PanthaRhei.Infrastructure.Tests.Logging
{
    public class LogManagerTests
    {
        private readonly LogManager logManager = new(new GenerationOptions { Root = "C:\\Some\\Root\\Path" });

        [Fact]
        public void Logger_ShouldBeOfCorrectType()
        {
            // arrange
            // act
            ILogger logger = logManager.Logger;

            // assert
            Assert.NotNull(logger);
            Assert.Equal(typeof(Logger), logger.GetType());
        }

        [Fact]
        public void Logger_ShouldBeSameInstance()
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

        [Fact]
        public void DefaultLogger_ShouldBeSameInstance()
        {
            // arrange
            // act
            ILogger logger1 = logManager.Logger;
            ILogger logger2 = logManager.Get(Loggers.DefaultLogger);

            // assert
            Assert.NotNull(logger1);
            Assert.NotNull(logger2);
            Assert.Same(logger1, logger2);
        }

        [Fact]
        public void AuthenticationLogger_ShouldBeSameInstance()
        {
            // arrange
            // act
            ILogger logger1 = logManager.GetAuthenticationLogger();
            ILogger logger2 = logManager.Get(Loggers.AuthenticationLogger);

            // assert
            Assert.NotNull(logger1);
            Assert.NotNull(logger2);
            Assert.Same(logger1, logger2);
        }

        [Fact]
        public void ExceptionLogger_ShouldBeSameInstance()
        {
            // arrange
            // act
            ILogger logger1 = logManager.GetExceptionLogger();
            ILogger logger2 = logManager.Get(Loggers.ExceptionLogger);

            // assert
            Assert.NotNull(logger1);
            Assert.NotNull(logger2);
            Assert.Same(logger1, logger2);
        }

        [Fact]
        public void InvalidLoggerName_ShouldThrowException()
        {
            // arrange
            string loggerName = "InvalidName";

            // act
            void Action() => logManager.Get(loggerName);

            // assert
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => Action());
            Assert.Equal($"Specified argument was out of the range of valid values. (Parameter 'loggerName')", ex.Message);
        }

        [Fact]
        public void LoggerNameNull_ShouldThrowException()
        {
            // arrange

            // act
            void Action() => logManager.Get(string.Empty);

            // assert
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => Action());
            Assert.Equal($"Value cannot be null. (Parameter 'loggerName')", ex.Message);
        }
    }
}
