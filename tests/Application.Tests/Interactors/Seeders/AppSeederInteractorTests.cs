using System;
using LiquidVisions.PanthaRhei.Application.Usecases.Seeders;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Interactors.Seeders
{
    public class AppSeederInteractorTests
    {
        private readonly Fakes fakes = new();
        private readonly AppSeeder interactor;
        private readonly Mock<ICreateRepository<App>> mockedCreateGateway = new();
        private readonly Mock<IDeleteRepository<App>> mockedDeleteGateway = new();

        public AppSeederInteractorTests()
        {
            fakes.IDependencyFactory.Setup(x => x.Get<ICreateRepository<App>>()).Returns(mockedCreateGateway.Object);
            fakes.IDependencyFactory.Setup(x => x.Get<IDeleteRepository<App>>()).Returns(mockedDeleteGateway.Object);

            interactor = new AppSeeder(fakes.IDependencyFactory.Object);
        }

        [Fact]
        public void Constructor_ShouldVerifyDependencies()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Get<ICreateRepository<App>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<IDeleteRepository<App>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(3));
            fakes.IDependencyFactory.Verify(x => x.GetAll<It.IsAnyType>(), Times.Never);
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

            fakes.GenerationOptions.Setup(x => x.AppId).Returns(appId);

            // act
            interactor.Seed(app);

            // assert
            fakes.GenerationOptions.Verify(x => x.AppId, Times.Once);
            mockedCreateGateway.Verify(x => x.Create(app), Times.Once);
            Assert.Equal(actualName, app.Name);
            Assert.Equal(actualFullName, app.FullName);
            Assert.Equal(appId, app.Id);
        }
    }
}
