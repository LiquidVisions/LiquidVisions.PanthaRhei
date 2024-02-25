using System;
using LiquidVisions.PanthaRhei.Application.Usecases.Seeders;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Usecases.Gateways
{
    /// <summary>
    /// Tests for <see cref="EntitiesToSeedGateway"/>.
    /// </summary>
    public class EntitiesToSeedGatewayTests
    {
        private readonly EntitiesToSeedGateway gateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntitiesToSeedGatewayTests"/> class.
        /// </summary>
        public EntitiesToSeedGatewayTests()
        {
            gateway = new EntitiesToSeedGateway();
        }

        /// <summary>
        /// Tests the <see cref="EntitiesToSeedGateway.GetById"/> method.
        /// </summary>
        [Fact]
        public void GetByIdShouldTrowNotImplementedException()
        {
            // arrange

            // act
            Func<object, Type> action = gateway.GetById;

            // assert
            Assert.Throws<NotImplementedException>(() => action(1));
        }
    }
}
