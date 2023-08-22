using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Parameters;
using Xunit;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests
{
    /// <summary>
    /// Tests for <seealso cref="RequestModelTemplateParameters"/>.
    /// </summary>
    public class RequestModelTemplateParametersTests
    {
        private readonly RequestModelTemplateParameters parameters = new ();

        /// <summary>
        /// Tests <seealso cref="RequestModelTemplateParameters.ElementType"/>.
        /// </summary>
        [Fact]
        public void ElementType_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal("RequestModel", parameters.ElementType);
        }

        /// <summary>
        /// Tests <seealso cref="RequestModelTemplateParameters.NamePostfix"/>.
        /// </summary>
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
