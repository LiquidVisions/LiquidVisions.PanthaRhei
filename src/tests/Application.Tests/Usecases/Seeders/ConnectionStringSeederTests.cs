using System.Linq;
using LiquidVisions.PanthaRhei.Application.Usecases.Seeders;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Usecases.Seeders
{
    /// <summary>
    /// Tests for <see cref="ConnectionStringsSeeder"/>.
    /// </summary>
    public class ConnectionStringSeederTests
    {
        private readonly Fakes fakes = new();
        private readonly ConnectionStringsSeeder interactor;
        private readonly Mock<ICreateRepository<ConnectionString>> mockedCreateGateway = new();
        private readonly Mock<IDeleteRepository<ConnectionString>> mockedDeleteGateway = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionStringSeederTests"/> class.
        /// </summary>
        public ConnectionStringSeederTests()
        {
            fakes.IDependencyFactory
                .Setup(x => x.Resolve<ICreateRepository<ConnectionString>>())
                .Returns(mockedCreateGateway.Object);
            fakes.IDependencyFactory
                .Setup(x => x.Resolve<IDeleteRepository<ConnectionString>>())
                .Returns(mockedDeleteGateway.Object);

            interactor = new ConnectionStringsSeeder(fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Tests that the constructor verifies dependencies.
        /// </summary>
        [Fact]
        public void ConstructorShouldVerifyDependencies()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Resolve<IDeleteRepository<ConnectionString>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<ICreateRepository<ConnectionString>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(2));
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<It.IsAnyType>(), Times.Never);
        }

        /// <summary>
        /// Tests for <see cref="ConnectionStringsSeeder.SeedOrder"/>.
        /// </summary>
        [Fact]
        public void SeedOrderShouldBe1()
        {
            // arrange
            // act
            // assert
            Assert.Equal(1, interactor.SeedOrder);
        }

        /// <summary>
        /// Tests for <see cref="ConnectionStringsSeeder.ResetOrder"/>.
        /// </summary>
        [Fact]
        public void ResetOrderShouldBe1()
        {
            // arrange
            // act
            // assert
            Assert.Equal(1, interactor.ResetOrder);
        }

        /// <summary>
        /// Test for <see cref="ConnectionStringsSeeder.Reset"/>.
        /// </summary>
        [Fact]
        public void ResetShouldVerify()
        {
            // arrange
            // act
            interactor.Reset();

            // assert
            mockedDeleteGateway.Verify(x => x.DeleteAll(), Times.Once);
        }

        /// <summary>
        /// Test for <see cref="ConnectionStringsSeeder.Seed"/>.
        /// </summary>
        [Fact]
        public void SeedShouldCreate()
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
