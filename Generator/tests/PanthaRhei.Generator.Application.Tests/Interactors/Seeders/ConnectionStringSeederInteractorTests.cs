using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.Application.Tests.Interactors.Seeders
{
    public class ConnectionStringSeederInteractorTests
    {
        private readonly Fakes fakes = new();
        private readonly ConnectionStringsSeederInteractor interactor;
        private readonly Mock<ICreateGateway<ConnectionString>> mockedCreateGateway = new();
        private readonly Mock<IDeleteGateway<ConnectionString>> mockedDeleteGateway = new();

        public ConnectionStringSeederInteractorTests()
        {
            fakes.IDependencyFactoryInteractor
                .Setup(x => x.Get<ICreateGateway<ConnectionString>>())
                .Returns(mockedCreateGateway.Object);
            fakes.IDependencyFactoryInteractor
                .Setup(x => x.Get<IDeleteGateway<ConnectionString>>())
                .Returns(mockedDeleteGateway.Object);

            interactor = new ConnectionStringsSeederInteractor(fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Constructor_ShouldVerifyDependencies()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IDeleteGateway<ConnectionString>>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ICreateGateway<ConnectionString>>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(2));
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
            App app = new();

            // act
            interactor.Seed(app);
            ConnectionString connectionString = app.ConnectionStrings.Single();

            // assert
            mockedCreateGateway.Verify(x => x.Create(connectionString), Times.Once);
            Assert.Equal(Resources.ConnectionStringName, connectionString.Name);
            Assert.Equal(Resources.ConnectionStringDefintion, connectionString.Definition);
            Assert.Equal(connectionString.App, app);
        }
    }
}
