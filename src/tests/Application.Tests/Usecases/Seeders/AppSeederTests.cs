using System;
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
    /// Tests for <see cref="AppSeeder"/>.
    /// </summary>
    public class AppSeederTests
    {
        private readonly Fakes fakes = new();
        private readonly AppSeeder interactor;
        private readonly Mock<ICreateRepository<App>> mockedCreateGateway = new();
        private readonly Mock<IDeleteRepository<App>> mockedDeleteGateway = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSeederTests"/> class.
        /// </summary>
        public AppSeederTests()
        {
            fakes.IDependencyFactory.Setup(x => x.Resolve<ICreateRepository<App>>()).Returns(mockedCreateGateway.Object);
            fakes.IDependencyFactory.Setup(x => x.Resolve<IDeleteRepository<App>>()).Returns(mockedDeleteGateway.Object);

            interactor = new AppSeeder(fakes.IDependencyFactory.Object);
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
            fakes.IDependencyFactory.Verify(x => x.Resolve<ICreateRepository<App>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IDeleteRepository<App>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(3));
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<It.IsAnyType>(), Times.Never);
        }

        /// <summary>
        /// Tests for <see cref="AppSeeder.Reset"/>.
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
        /// Tests for <see cref="AppSeeder.ResetOrder"/>.
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
        /// Tests for <see cref="AppSeeder.Reset"/>.
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
        /// Tests for <see cref="AppSeeder.Seed"/>.
        /// </summary>
        [Fact]
        public void SeedShouldCreate()
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
