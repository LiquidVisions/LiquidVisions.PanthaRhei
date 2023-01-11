using System;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.Application.Tests.Interactors.Seeders
{
    public class AppSeederInteractorTests : AbstractTests
    {
        private readonly AppSeederInteractor interactor;
        private readonly Mock<IGenericGateway<App>> mockedIGenericGatewayForApp = new Mock<IGenericGateway<App>>();

        public AppSeederInteractorTests()
        {
            Fakes.IDependencyFactoryInteractor.Setup(x => x.Get<IGenericGateway<App>>()).Returns(mockedIGenericGatewayForApp.Object);

            interactor = new AppSeederInteractor(Fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Constructor_ShouldVerifyDependencies()
        {
            // arrange
            // act
            // assert
            Fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IGenericGateway<App>>(), Times.Once);
            Fakes.IDependencyFactoryInteractor.Verify(x => x.Get<Parameters>(), Times.Once);
            Fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(2));
            Fakes.IDependencyFactoryInteractor.Verify(x => x.GetAll<It.IsAnyType>(), Times.Never);
        }

        [Fact]
        public void SeedOrder_ShouldBe1()
        {
            // arrange
            // act
            // assert
            Assert.Equal(1, interactor.SeedOrder);
        }

        [Fact]
        public void ResetOrder_ShouldBe1()
        {
            // arrange
            // act
            // assert
            Assert.Equal(1, interactor.ResetOrder);
        }

        [Fact]
        public void Reset_ShouldVerify()
        {
            // arrange
            // act
            interactor.Reset();

            // assert
            mockedIGenericGatewayForApp.Verify(x => x.DeleteAll(), Times.Once);
        }

        [Fact]
        public void Seed_ShouldCreate()
        {
            // arrange
            const string actualName = "PanthaRhei.Generated";
            const string actualFullName = "LiquidVisions.PanthaRhei.Generated";
            Guid appId = Guid.NewGuid();
            App app = new();

            Fakes.Parameters.Setup(x => x.AppId).Returns(appId);

            // act
            interactor.Seed(app);

            // assert
            Fakes.Parameters.Verify(x => x.AppId, Times.Once);
            mockedIGenericGatewayForApp.Verify(x => x.Create(app), Times.Once);
            Assert.Equal(actualName, app.Name);
            Assert.Equal(actualFullName, app.FullName);
            Assert.Equal(appId, app.Id);
        }
    }
}
