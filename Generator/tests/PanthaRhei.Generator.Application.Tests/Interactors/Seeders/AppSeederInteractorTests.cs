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
    public class AppSeederInteractorTests
    {
        private readonly Fakes fakes = new();
        private readonly AppSeederInteractor interactor;
        private readonly Mock<ICreateGateway<App>> mockedCreateGateway = new();
        private readonly Mock<IDeleteGateway<App>> mockedDeleteGateway = new();

        public AppSeederInteractorTests()
        {
            fakes.IDependencyFactoryInteractor.Setup(x => x.Get<ICreateGateway<App>>()).Returns(mockedCreateGateway.Object);
            fakes.IDependencyFactoryInteractor.Setup(x => x.Get<IDeleteGateway<App>>()).Returns(mockedDeleteGateway.Object);

            interactor = new AppSeederInteractor(fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Constructor_ShouldVerifyDependencies()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ICreateGateway<App>>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IDeleteGateway<App>>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ExpandRequestModel>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(3));
            fakes.IDependencyFactoryInteractor.Verify(x => x.GetAll<It.IsAnyType>(), Times.Never);
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
            mockedDeleteGateway.Verify(x => x.DeleteAll(), Times.Once);
        }

        [Fact]
        public void Seed_ShouldCreate()
        {
            // arrange
            const string actualName = "PanthaRhei.Generated";
            const string actualFullName = "LiquidVisions.PanthaRhei.Generated";
            Guid appId = Guid.NewGuid();
            App app = new();

            fakes.Parameters.Setup(x => x.AppId).Returns(appId);

            // act
            interactor.Seed(app);

            // assert
            fakes.Parameters.Verify(x => x.AppId, Times.Once);
            mockedCreateGateway.Verify(x => x.Create(app), Times.Once);
            Assert.Equal(actualName, app.Name);
            Assert.Equal(actualFullName, app.FullName);
            Assert.Equal(appId, app.Id);
        }
    }
}
