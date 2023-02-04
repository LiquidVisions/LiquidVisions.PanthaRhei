using System;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Gateways;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.Application.Tests.Interactors.Gateways
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
