using LiquidVisions.PanthaRhei.Infrastructure.Logging;
using Xunit;

namespace LiquidVisions.PanthaRhei.Infrastructure.Tests.Logging
{
    /// <summary>
    /// Tests for <see cref="Loggers"/>.
    /// </summary>
    public class LoggersTests
    {
        /// <summary>
        /// Properties should contain correct values.
        /// </summary>
        [Fact]
        public void LoggersConstantsShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal("DefaultLogger", Loggers.DefaultLogger);
            Assert.Equal("AuthenticationLogger", Loggers.AuthenticationLogger);
            Assert.Equal("ExceptionLogger", Loggers.ExceptionLogger);
        }
    }
}
