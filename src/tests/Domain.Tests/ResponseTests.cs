using System.Linq;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Domain.Tests
{
    /// <summary>
    /// Tests for the Response class
    /// </summary>
    public class ResponseTests
    {
        /// <summary>
        /// Test to verify that the default value of IsValid is true
        /// </summary>
        [Fact]
        public void ResponseIsValidDefaultValueShouldBeTrue()
        {
            // arrange
            // act
            // assert
            Assert.True(new Response().IsValid);
        }

        /// <summary>
        /// Test to verify that adding an error to the response will lead to IsValid being false
        /// </summary>
        [Fact]
        public void ResponseAddingErrorLeadsToIsValidFalse()
        {
            // arrange
            Response response = new();

            // act
            response.AddError(FaultCodes.UnAuthorized, "test message");

            // assert
            Assert.False(response.IsValid);
            Assert.Single(response.Errors);
            Assert.Equal(FaultCodes.UnAuthorized, response.Errors.First().FaultCode);
            Assert.Equal("test message", response.Errors.First().FaultMessage);
        }

        /// <summary>
        /// Tests the code and message of the FaultCodes enum
        /// </summary>
        [Fact]
        public void FaultCodeTests()
        {
            // arrange
            // act
            // assert
            Assert.Equal(401, FaultCodes.UnAuthorized.Code);
            Assert.Equal("Unauthorized", FaultCodes.UnAuthorized.Message);

            Assert.Equal(404, FaultCodes.NotFound.Code);
            Assert.Equal("NotFound", FaultCodes.NotFound.Message);

            Assert.Equal(500, FaultCodes.InternalServerError.Code);
            Assert.Equal("Internal Server Error", FaultCodes.InternalServerError.Message);

            Assert.Equal(400, FaultCodes.BadRequest.Code);
            Assert.Equal("Bad Request", FaultCodes.BadRequest.Message);
        }

        /// <summary>
        /// Test to verify that the default value of the generic parameter is true
        /// </summary>
        [Fact]
        public void ResponseWithGenericParameterTest()
        {
            // arrange
            Response<string> response = new();
            string value = "test";

            // act
            response.Parameter = value;

            // assert
            Assert.Equal(value, response.Parameter);
        }
    }
}
