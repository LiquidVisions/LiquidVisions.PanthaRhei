using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Parameters;
using Xunit;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests
{
    public class RequestModelTemplateParametersTests
    {
        private readonly RequestModelTemplateParameters parameters = new();

        [Fact]
        public void ElementType_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal("RequestModel", parameters.ElementType);
        }

        [Fact]
        public void NamePostfix_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal("RequestModel", parameters.NamePostfix);
        }
    }
}
