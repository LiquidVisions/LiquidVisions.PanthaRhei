using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Application.Usecases.Seeders;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Usecases.Seeders
{
    /// <summary>
    /// Tests for the <seealso cref="ComponentSeeder"/>.
    /// </summary>
    public class ComponentSeederTests
    {
        private readonly Fakes fakes = new();
        private readonly ComponentSeeder interactor;
        private readonly Mock<ICreateRepository<Component>> mockedCreateGateway = new();
        private readonly Mock<IDeleteRepository<Component>> mockedDeleteGateway = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentSeederTests"/> class.
        /// </summary>
        public ComponentSeederTests()
        {
            fakes.IDependencyFactory.Setup(x => x.Resolve<ICreateRepository<Component>>()).Returns(mockedCreateGateway.Object);
            fakes.IDependencyFactory.Setup(x => x.Resolve<IDeleteRepository<Component>>()).Returns(mockedDeleteGateway.Object);

            interactor = new ComponentSeeder(fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Dependency tests.
        /// </summary>
        [Fact]
        public void ConstructorShouldVerifyDependencies()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Resolve<ICreateRepository<Component>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IDeleteRepository<Component>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(2));
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<It.IsAnyType>(), Times.Never);
        }

        /// <summary>
        /// Test for <seealso cref="ComponentSeeder.SeedOrder"/>.
        /// </summary>
        [Fact]
        public void SeedOrderShouldBe1()
        {
            // arrange
            // act
            // assert
            Assert.Equal(3, interactor.SeedOrder);
        }


        /// <summary>
        /// Test for <seealso cref="ComponentSeeder.ResetOrder"/>.
        /// </summary>
        [Fact]
        public void ResetOrderShouldBe1()
        {
            // arrange
            // act
            // assert
            Assert.Equal(3, interactor.ResetOrder);
        }


        /// <summary>
        /// Test for <seealso cref="ComponentSeeder.Reset"/>.
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
        /// Test for <seealso cref="ComponentSeeder.Seed(App)"/> happyflow.
        /// </summary>
        [Fact]
        public void ShouldSeedComponentWithTheCorrectName()
        {
            // arrange
            Expander expander1 = new() { Name = "Expander1", Enabled = true };
            Expander expander2 = new() { Name = "One.Two", Enabled = true };
            Expander expander3 = new() { Name = "One.Two.Three", Enabled = true };

            App app = new();
            app.Expanders.Add(expander1);
            app.Expanders.Add(expander2);
            app.Expanders.Add(expander3);

            // act
            interactor.Seed(app);

            // assert
            mockedCreateGateway.Verify(x => x.Create(It.Is<Component>(c => c.Name == "Expander1" && c.Expander == expander1)), Times.Once);
            mockedCreateGateway.Verify(x => x.Create(It.Is<Component>(c => c.Name == "Two" && c.Expander == expander2)), Times.Once);
            mockedCreateGateway.Verify(x => x.Create(It.Is<Component>(c => c.Name == "Two.Three" && c.Expander == expander3)), Times.Once);
        }
    }
}
