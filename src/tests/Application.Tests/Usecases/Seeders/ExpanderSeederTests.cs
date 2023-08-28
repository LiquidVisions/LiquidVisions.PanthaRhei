using System;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Application.Usecases.Initializers;
using LiquidVisions.PanthaRhei.Application.Usecases.Seeders;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Usecases.Seeders
{
    /// <summary>
    /// Tests for <seealso cref="ExpanderSeeder"/>.
    /// </summary>
    public class ExpanderSeederTests
    {
        private readonly ExpanderSeeder _interactor;
        private readonly ApplicationFakes _fakes = new();
        private readonly Mock<IDeleteRepository<Expander>> _deleteGateWay = new();
        private readonly Mock<ICreateRepository<Expander>> _createGateWay = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpanderSeederTests"/> class.
        /// </summary>
        public ExpanderSeederTests()
        {
            _fakes.IDependencyFactory.Setup(x => x.Get<IDeleteRepository<Expander>>()).Returns(_deleteGateWay.Object);
            _fakes.IDependencyFactory.Setup(x => x.Get<ICreateRepository<Expander>>()).Returns(_createGateWay.Object);

            _interactor = new(_fakes.IDependencyFactory.Object);
        }


        /// <summary>
        /// Dependency Test.
        /// </summary>
        [Fact]
        public void Constructor_ShouldResolve()
        {
            // arrange
            // act
            // assert
            _fakes.IDependencyFactory.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(4));
            _fakes.IDependencyFactory.Verify(x => x.Get<IDeleteRepository<Expander>>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Get<ICreateRepository<Expander>>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Get<IExpanderPluginLoader>(), Times.Once);
        }

        /// <summary>
        /// Test for <seealso cref="ExpanderSeeder.SeedOrder"/>.
        /// </summary>
        [Fact]
        public void SeedOrder_ShouldBe()
        {
            // arrange
            // act
            // assert
            Assert.Equal(2, _interactor.SeedOrder);
        }

        /// <summary>
        /// Test for <seealso cref="ExpanderSeeder.ResetOrder"/>.
        /// </summary>
        [Fact]
        public void ResetOrder_ShouldBe()
        {
            // arrange
            // act
            // assert
            Assert.Equal(4, _interactor.ResetOrder);
        }

        /// <summary>
        /// Test for <seealso cref="ExpanderSeeder.Reset"/>.
        /// </summary>
        [Fact]
        public void Reset_ShouldDeleteAll()
        {
            // arrange

            // act
            _interactor.Reset();

            // assert
            _deleteGateWay.Verify(x => x.DeleteAll(), Times.Once);
        }

        /// <summary>
        /// Test for <seealso cref="ExpanderSeeder.Seed(App)"/>.
        /// </summary>
        [Fact]
        public void Seed_ShouldVerify()
        {
            // arrange
            App app = new();
            Mock<IExpander> mockedExpanderInteractor = new();
            mockedExpanderInteractor.Setup(x => x.Name).Returns("RandomName");
            mockedExpanderInteractor.Setup(x => x.Order).Returns(2);
            _fakes.IExpanderPluginLoader.Setup(x => x.ShallowLoadAllExpanders(_fakes.GenerationOptions.Object.ExpandersFolder)).Returns(new List<IExpander> { mockedExpanderInteractor.Object });

            // act
            _interactor.Seed(app);

            // assert
            _fakes.IExpanderPluginLoader.Verify(x => x.ShallowLoadAllExpanders(_fakes.GenerationOptions.Object.ExpandersFolder), Times.Once);
            Assert.Single(app.Expanders);
            Assert.Same(app.Expanders.Single().Apps.Single(), app);
            _createGateWay.Verify(x => x.Create(It.IsAny<Expander>()), Times.Once);
            _createGateWay.Verify(x => x.Create(It.Is<Expander>(x => x.Id != Guid.Empty && x.Name == "RandomName" && x.Order == 2 && x.Enabled && x.Apps.Single() == app)), Times.Once);
        }
    }
}
