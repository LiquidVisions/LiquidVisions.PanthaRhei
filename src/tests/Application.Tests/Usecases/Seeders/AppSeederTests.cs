﻿using System;
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
        private readonly Fakes _fakes = new();
        private readonly AppSeeder _interactor;
        private readonly Mock<ICreateRepository<App>> _mockedCreateGateway = new();
        private readonly Mock<IDeleteRepository<App>> _mockedDeleteGateway = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSeederTests"/> class.
        /// </summary>
        public AppSeederTests()
        {
            _fakes.IDependencyFactory.Setup(x => x.Get<ICreateRepository<App>>()).Returns(_mockedCreateGateway.Object);
            _fakes.IDependencyFactory.Setup(x => x.Get<IDeleteRepository<App>>()).Returns(_mockedDeleteGateway.Object);

            _interactor = new AppSeeder(_fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Tests that the constructor verifies dependencies.
        /// </summary>
        [Fact]
        public void Constructor_ShouldVerifyDependencies()
        {
            // arrange
            // act
            // assert
            _fakes.IDependencyFactory.Verify(x => x.Get<ICreateRepository<App>>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Get<IDeleteRepository<App>>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(3));
            _fakes.IDependencyFactory.Verify(x => x.GetAll<It.IsAnyType>(), Times.Never);
        }

        /// <summary>
        /// Tests for <see cref="AppSeeder.Reset"/>.
        /// </summary>
        [Fact]
        public void SeedOrder_ShouldBe1()
        {
            // arrange
            // act
            // assert
            Assert.Equal(1, _interactor.SeedOrder);
        }

        /// <summary>
        /// Tests for <see cref="AppSeeder.ResetOrder"/>.
        /// </summary>
        [Fact]
        public void ResetOrder_ShouldBe1()
        {
            // arrange
            // act
            // assert
            Assert.Equal(1, _interactor.ResetOrder);
        }

        /// <summary>
        /// Tests for <see cref="AppSeeder.Reset"/>.
        /// </summary>
        [Fact]
        public void Reset_ShouldVerify()
        {
            // arrange
            // act
            _interactor.Reset();

            // assert
            _mockedDeleteGateway.Verify(x => x.DeleteAll(), Times.Once);
        }

        /// <summary>
        /// Tests for <see cref="AppSeeder.Seed"/>.
        /// </summary>
        [Fact]
        public void Seed_ShouldCreate()
        {
            // arrange
            const string actualName = "PanthaRhei.Generated";
            const string actualFullName = "LiquidVisions.PanthaRhei.Generated";
            Guid appId = Guid.NewGuid();
            App app = new();

            _fakes.GenerationOptions.Setup(x => x.AppId).Returns(appId);

            // act
            _interactor.Seed(app);

            // assert
            _fakes.GenerationOptions.Verify(x => x.AppId, Times.Once);
            _mockedCreateGateway.Verify(x => x.Create(app), Times.Once);
            Assert.Equal(actualName, app.Name);
            Assert.Equal(actualFullName, app.FullName);
            Assert.Equal(appId, app.Id);
        }
    }
}
