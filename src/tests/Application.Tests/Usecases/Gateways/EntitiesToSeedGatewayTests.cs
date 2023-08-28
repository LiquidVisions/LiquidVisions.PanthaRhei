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
        private readonly EntitiesToSeedGateway _gateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntitiesToSeedGatewayTests"/> class.
        /// </summary>
        public EntitiesToSeedGatewayTests()
        {
            _gateway = new EntitiesToSeedGateway();
        }

        /// <summary>
        /// Tests the <see cref="EntitiesToSeedGateway.GetById"/> method.
        /// </summary>
        [Fact]
        public void GetById_ShouldTrowNotImplementedException()
        {
            // arrange

            // act
            Func<object, Type> action = _gateway.GetById;

            // assert
            Assert.Throws<NotImplementedException>(() => action(1));
        }
    }
}
