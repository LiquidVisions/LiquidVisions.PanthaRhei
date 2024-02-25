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
        private readonly ExpanderSeeder interactor;
        private readonly ApplicationFakes fakes = new();
        private readonly Mock<IDeleteRepository<Expander>> deleteGateWay = new();
        private readonly Mock<ICreateRepository<Expander>> createGateWay = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpanderSeederTests"/> class.
        /// </summary>
        public ExpanderSeederTests()
        {
            fakes.IDependencyFactory.Setup(x => x.Resolve<IDeleteRepository<Expander>>()).Returns(deleteGateWay.Object);
            fakes.IDependencyFactory.Setup(x => x.Resolve<ICreateRepository<Expander>>()).Returns(createGateWay.Object);

            interactor = new(fakes.IDependencyFactory.Object);
        }


        /// <summary>
        /// Dependency Test.
        /// </summary>
        [Fact]
        public void ConstructorShouldResolve()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(4));
            fakes.IDependencyFactory.Verify(x => x.Resolve<IDeleteRepository<Expander>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<ICreateRepository<Expander>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IExpanderPluginLoader>(), Times.Once);
        }

        /// <summary>
        /// Test for <seealso cref="ExpanderSeeder.SeedOrder"/>.
        /// </summary>
        [Fact]
        public void SeedOrderShouldBe()
        {
            // arrange
            // act
            // assert
            Assert.Equal(2, interactor.SeedOrder);
        }

        /// <summary>
        /// Test for <seealso cref="ExpanderSeeder.ResetOrder"/>.
        /// </summary>
        [Fact]
        public void ResetOrderShouldBe()
        {
            // arrange
            // act
            // assert
            Assert.Equal(4, interactor.ResetOrder);
        }

        /// <summary>
        /// Test for <seealso cref="ExpanderSeeder.Reset"/>.
        /// </summary>
        [Fact]
        public void ResetShouldDeleteAll()
        {
            // arrange

            // act
            interactor.Reset();

            // assert
            deleteGateWay.Verify(x => x.DeleteAll(), Times.Once);
        }

        /// <summary>
        /// Test for <seealso cref="ExpanderSeeder.Seed(App)"/>.
        /// </summary>
        [Fact]
        public void SeedShouldVerify()
        {
            // arrange
            App app = new();
            Mock<IExpander> mockedExpanderInteractor = new();
            mockedExpanderInteractor.Setup(x => x.Name).Returns("RandomName");
            mockedExpanderInteractor.Setup(x => x.Order).Returns(2);
            fakes.IExpanderPluginLoader.Setup(x => x.ShallowLoadAllExpanders(fakes.GenerationOptions.Object.ExpandersFolder)).Returns(new List<IExpander> { mockedExpanderInteractor.Object });

            // act
            interactor.Seed(app);

            // assert
            fakes.IExpanderPluginLoader.Verify(x => x.ShallowLoadAllExpanders(fakes.GenerationOptions.Object.ExpandersFolder), Times.Once);
            Assert.Single(app.Expanders);
            Assert.Same(app.Expanders.Single().Apps.Single(), app);
            createGateWay.Verify(x => x.Create(It.IsAny<Expander>()), Times.Once);
            createGateWay.Verify(x => x.Create(It.Is<Expander>(x => x.Id != Guid.Empty && x.Name == "RandomName" && x.Order == 2 && x.Enabled && x.Apps.Single() == app)), Times.Once);
        }
    }
}
