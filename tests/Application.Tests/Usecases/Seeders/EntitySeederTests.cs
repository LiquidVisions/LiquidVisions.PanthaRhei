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
    public class EntitySeederTests
    {
        private readonly Fakes fakes = new();
        private readonly EntitySeeder interactor;
        private readonly Mock<IDeleteRepository<Entity>> mockedDeleteGateway = new();
        private readonly Mock<ICreateRepository<Entity>> mockedCreateGateway = new();
        private readonly Mock<IEntitiesToSeedRepository> mockedEntityToSeedGateway = new();

        public EntitySeederTests()
        {
            fakes.IDependencyFactory.Setup(x => x.Get<ICreateRepository<Entity>>()).Returns(mockedCreateGateway.Object);
            fakes.IDependencyFactory.Setup(x => x.Get<IDeleteRepository<Entity>>()).Returns(mockedDeleteGateway.Object);
            fakes.IDependencyFactory.Setup(x => x.Get<IEntitiesToSeedRepository>()).Returns(mockedEntityToSeedGateway.Object);

            interactor = new EntitySeeder(fakes.IDependencyFactory.Object);
        }

        [Fact]
        public void Constructor_ShouldVerifyDependencies()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Get<IDeleteRepository<Entity>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<ICreateRepository<Entity>>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<IEntitiesToSeedRepository>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(3));
            fakes.IDependencyFactory.Verify(x => x.GetAll<It.IsAnyType>(), Times.Never);
        }

        [Fact]
        public void SeedOrder_ShouldBe1()
        {
            // arrange
            // act
            // assert
            Assert.Equal(5, interactor.SeedOrder);
        }

        [Fact]
        public void ResetOrder_ShouldBe1()
        {
            // arrange
            // act
            // assert
            Assert.Equal(5, interactor.ResetOrder);
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
            MockEntityToSeerderGetAll(new Type[] { typeof(PublicClassSet) });

            // act
            interactor.Seed(app);

            // assert
            mockedCreateGateway.Verify(x => x.Create(It.IsAny<Entity>()), Times.Once);
        }

        [Fact]
        public void Seed_NameShouldValidate()
        {
            // arrange
            App app = new();
            MockEntityToSeerderGetAll(new Type[] { typeof(PublicClassSet) });

            // act
            interactor.Seed(app);

            // assert
            mockedCreateGateway.Verify(x => x.Create(It.Is<Entity>(x => x.Name == nameof(PublicClassSet) && x.Id != Guid.Empty)), Times.Once);
        }

        [Fact]
        public void Seed_Callsite_ShouldValidate()
        {
            // arrange
            App app = new() { FullName = "App.Full.Name" };
            MockEntityToSeerderGetAll(new Type[] { typeof(PublicClassSet) });

            // act
            interactor.Seed(app);

            // assert
            mockedCreateGateway.Verify(x => x.Create(It.Is<Entity>(x => x.Callsite == $"{app.FullName}.Domain.Entities")), Times.Once);
        }

        [Fact]
        public void Seed_AppSet_ShouldValidate()
        {
            // arrange
            App app = new() { FullName = "App.Full.Name" };
            MockEntityToSeerderGetAll(new Type[] { typeof(PublicClassSet) });

            // act
            interactor.Seed(app);

            // assert
            mockedCreateGateway.Verify(x => x.Create(It.Is<Entity>(x => x.App == app)), Times.Once);
            Assert.Single(app.Entities.Where(x => x.App == app));
        }

        [Theory]
        [InlineData(typeof(ProptectedClass), "protected")]
        [InlineData(typeof(PrivateClass), "private")]
        [InlineData(typeof(PublicClassSet), "public")]
        public void Seed_Modifier_ShouldValidate(Type type, string expectedResult)
        {
            // arrange
            App app = new() { };
            MockEntityToSeerderGetAll(new Type[] { type });

            // act
            interactor.Seed(app);

            // assert
            mockedCreateGateway.Verify(x => x.Create(It.Is<Entity>(x => x.Modifier == expectedResult)), Times.Once);
        }

        [Theory]
        [InlineData(typeof(AbstractClass), "abstract")]
        [InlineData(typeof(PublicClassSet), null)]
        public void Seed_Behaviour_ShouldValidate(Type type, string expectedResult)
        {
            // arrange
            App app = new() { };
            MockEntityToSeerderGetAll(new Type[] { type });

            // act
            interactor.Seed(app);

            // assert
            mockedCreateGateway.Verify(x => x.Create(It.Is<Entity>(x => x.Behaviour == expectedResult)), Times.Once);
        }

        [Theory]
        [InlineData(typeof(PublicClassSet), "class")]
        [InlineData(typeof(IPublicInterface), "interface")]
        [InlineData(typeof(EnumTest), "enum")]
        public void Seed_Type_ShouldValidate(Type type, string expectedResult)
        {
            // arrange
            App app = new() { };
            MockEntityToSeerderGetAll(new Type[] { type });

            // act
            interactor.Seed(app);

            // assert
            mockedCreateGateway.Verify(x => x.Create(It.Is<Entity>(x => x.Type == expectedResult)), Times.Once);
        }

        private void MockEntityToSeerderGetAll(Type[] types)
        {
            mockedEntityToSeedGateway
                .Setup(x => x.GetAll())
                .Returns(types);
        }

        [SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "Used for testing")]
        [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "suppress")]
        protected class ProptectedClass
        {
        }

        [SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "Used for testing")]
        [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "suppress")]
        private class PrivateClass
        {
        }
    }
}
