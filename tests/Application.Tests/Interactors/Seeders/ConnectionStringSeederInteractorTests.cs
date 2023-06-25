using System.Linq;
using LiquidVisions.PanthaRhei.Application.Usecases.Seeders;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Interactors.Seeders
{
    public class ConnectionStringSeederInteractorTests
    {
        private readonly Fakes fakes = new();
        private readonly ConnectionStringsSeeder interactor;
        private readonly Mock<ICreateRepository<ConnectionString>> mockedCreateGateway = new();
        private readonly Mock<IDeleteRepository<ConnectionString>> mockedDeleteGateway = new();

        public ConnectionStringSeederInteractorTests()
        {
            fakes.IDependencyFactory
                .Setup(x => x.Get<ICreateRepository<ConnectionString>>())
                .Returns(mockedCreateGateway.Object);
            fakes.IDependencyFactory
                .Setup(x => x.Get<IDeleteRepository<ConnectionString>>())
                .Returns(mockedDeleteGateway.Object);

            interactor = new ConnectionStringsSeeder(fakes.IDependencyFactory.Object);
        }

        [Fact]
        public void Constructor_ShouldVerifyDependencies()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Get<IDeleteRepository<ConnectionString>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<ICreateRepository<ConnectionString>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(2));
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
