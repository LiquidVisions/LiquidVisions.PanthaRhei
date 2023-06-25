using System;
using LiquidVisions.PanthaRhei.Application.Usecases.Seeders;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Interactors.Gateways
{
    public class EntitiesToSeedGatewayTests
    {
        private readonly EntitiesToSeedGateway gateway;

        public EntitiesToSeedGatewayTests()
        {
            gateway = new EntitiesToSeedGateway();
        }

        [Fact]
        public void GetById_ShouldTrowNotImplementedException()
        {
            // arrange

            // act
            Func<object, Type> action = gateway.GetById;

            // assert
            Assert.Throws<NotImplementedException>(() => action(1));
        }
    }
}
