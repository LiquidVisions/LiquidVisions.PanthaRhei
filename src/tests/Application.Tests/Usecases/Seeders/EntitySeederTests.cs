using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using LiquidVisions.PanthaRhei.Application.Tests.Mocks;
using LiquidVisions.PanthaRhei.Application.Usecases.Seeders;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Usecases.Seeders
{
    /// <summary>
    /// Tests for <see cref="EntitySeeder"/>.
    /// </summary>
    public class EntitySeederTests
    {
        private readonly Fakes _fakes = new();
        private readonly EntitySeeder _interactor;
        private readonly Mock<IDeleteRepository<Entity>> _mockedDeleteGateway = new();
        private readonly Mock<ICreateRepository<Entity>> _mockedCreateGateway = new();
        private readonly Mock<IEntitiesToSeedRepository> _mockedEntityToSeedGateway = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="EntitySeederTests"/> class.
        /// </summary>
        public EntitySeederTests()
        {
            _fakes.IDependencyFactory.Setup(x => x.Resolve<ICreateRepository<Entity>>()).Returns(_mockedCreateGateway.Object);
            _fakes.IDependencyFactory.Setup(x => x.Resolve<IDeleteRepository<Entity>>()).Returns(_mockedDeleteGateway.Object);
            _fakes.IDependencyFactory.Setup(x => x.Resolve<IEntitiesToSeedRepository>()).Returns(_mockedEntityToSeedGateway.Object);

            _interactor = new EntitySeeder(_fakes.IDependencyFactory.Object);
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
            _fakes.IDependencyFactory.Verify(x => x.Resolve<IDeleteRepository<Entity>>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<ICreateRepository<Entity>>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<IEntitiesToSeedRepository>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(3));
            _fakes.IDependencyFactory.Verify(x => x.ResolveAll<It.IsAnyType>(), Times.Never);
        }

        /// <summary>
        /// Test for <see cref="EntitySeeder.SeedOrder"/>.
        /// </summary>
        [Fact]
        public void SeedOrderShouldBe5()
        {
            // arrange
            // act
            // assert
            Assert.Equal(5, _interactor.SeedOrder);
        }

        /// <summary>
        /// Test for <see cref="EntitySeeder.ResetOrder"/>.
        /// </summary>
        [Fact]
        public void ResetOrderShouldBe5()
        {
            // arrange
            // act
            // assert
            Assert.Equal(5, _interactor.ResetOrder);
        }

        /// <summary>
        /// Test for <see cref="EntitySeeder.Reset"/>.
        /// </summary>
        [Fact]
        public void ResetShouldVerify()
        {
            // arrange
            // act
            _interactor.Reset();

            // assert
            _mockedDeleteGateway.Verify(x => x.DeleteAll(), Times.Once);
        }

        /// <summary>
        /// Test for <see cref="EntitySeeder.Seed"/>.
        /// </summary>
        [Fact]
        public void SeedShouldCreate()
        {
            // arrange
            App app = new();
            MockEntityToSeederGetAll([typeof(PublicClassSet)]);

            // act
            _interactor.Seed(app);

            // assert
            _mockedCreateGateway.Verify(x => x.Create(It.IsAny<Entity>()), Times.Once);
        }

        /// <summary>
        /// Test for <see cref="EntitySeeder.Seed"/> testing the name of the Seeder.
        /// </summary>
        [Fact]
        public void SeedNameShouldValidate()
        {
            // arrange
            App app = new();
            MockEntityToSeederGetAll([typeof(PublicClassSet)]);

            // act
            _interactor.Seed(app);

            // assert
            _mockedCreateGateway.Verify(x => x.Create(It.Is<Entity>(x => x.Name == nameof(PublicClassSet) && x.Id != Guid.Empty)), Times.Once);
        }

        /// <summary>
        /// Test for <see cref="EntitySeeder.Seed"/> testing the call site of the Seeder.
        /// </summary>
        [Fact]
        public void SeedCallSiteShouldValidate()
        {
            // arrange
            App app = new() { FullName = "App.Full.Name" };
            MockEntityToSeederGetAll([typeof(PublicClassSet)]);

            // act
            _interactor.Seed(app);

            // assert
            _mockedCreateGateway.Verify(x => x.Create(It.Is<Entity>(x => x.Callsite == $"{app.FullName}.Domain.Entities")), Times.Once);
        }

        /// <summary>
        /// Test for <see cref="EntitySeeder.Seed"/> testing the app of the Seeder.
        /// </summary>
        [Fact]
        public void SeedAppSetShouldValidate()
        {
            // arrange
            App app = new() { FullName = "App.Full.Name" };
            MockEntityToSeederGetAll([typeof(PublicClassSet)]);

            // act
            _interactor.Seed(app);

            // assert
            _mockedCreateGateway.Verify(x => x.Create(It.Is<Entity>(x => x.App == app)), Times.Once);
            Assert.Single(app.Entities.Where(x => x.App == app));
        }

        /// <summary>
        /// Test for <see cref="EntitySeeder.Seed"/> testing the modifier of the Seeder.
        /// </summary>
        /// <param name="type"><seealso cref="Type"/> of tte entity</param>
        /// <param name="expectedResult">Expected result.</param>
        [Theory]
        [InlineData(typeof(ProtectedClass), "protected")]
        [InlineData(typeof(PrivateClass), "private")]
        [InlineData(typeof(PublicClassSet), "public")]
        public void SeedModifierShouldValidate(Type type, string expectedResult)
        {
            // arrange
            App app = new() { };
            MockEntityToSeederGetAll([type]);

            // act
            _interactor.Seed(app);

            // assert
            _mockedCreateGateway.Verify(x => x.Create(It.Is<Entity>(x => x.Modifier == expectedResult)), Times.Once);
        }

        /// <summary>
        /// Test for <see cref="EntitySeeder.Seed"/> testing the behaviour of the Seeder.
        /// </summary>
        /// <param name="type"><seealso cref="Type"/> of tte entity</param>
        /// <param name="expectedResult">Expected result.</param>
        [Theory]
        [InlineData(typeof(AbstractClass), "abstract")]
        [InlineData(typeof(PublicClassSet), null)]
        public void SeedBehaviourShouldValidate(Type type, string expectedResult)
        {
            // arrange
            App app = new() { };
            MockEntityToSeederGetAll([type]);

            // act
            _interactor.Seed(app);

            // assert
            _mockedCreateGateway.Verify(x => x.Create(It.Is<Entity>(x => x.Behaviour == expectedResult)), Times.Once);
        }

        /// <summary>
        /// Test for <see cref="EntitySeeder.Seed"/> testing the Type of the Seeder.
        /// </summary>
        /// <param name="type"><seealso cref="Type"/> of tte entity</param>
        /// <param name="expectedResult">Expected result.</param>
        [Theory]
        [InlineData(typeof(PublicClassSet), "class")]
        [InlineData(typeof(IPublicInterface), "interface")]
        [InlineData(typeof(EnumTest), "enum")]
        public void SeedTypeShouldValidate(Type type, string expectedResult)
        {
            // arrange
            App app = new() { };
            MockEntityToSeederGetAll([type]);

            // act
            _interactor.Seed(app);

            // assert
            _mockedCreateGateway.Verify(x => x.Create(It.Is<Entity>(x => x.Type == expectedResult)), Times.Once);
        }

        private void MockEntityToSeederGetAll(Type[] types)
        {
            _mockedEntityToSeedGateway
                .Setup(x => x.GetAll())
                .Returns(types);
        }

        /// <summary>
        /// mock class used for testing.
        /// </summary>
        protected class ProtectedClass
        {
        }

        private class PrivateClass
        {
        }
    }
}
