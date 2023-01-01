using LiquidVisions.PanthaRhei.Generator.Infrastructure.Logging;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.Tests.Logging
{
    public class LoggersTests
    {
        [Fact]
        public void Loggers_ConstantsShouldBeEqual()
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
