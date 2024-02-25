using LiquidVisions.PanthaRhei.Application.Usecases.Seeders;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Usecases.Seeders
{
    /// <summary>
    /// Tests for <see cref="Seeder"/>.
    /// </summary>
    public class SeederTests
    {
        private readonly ApplicationFakes fakes = new();

        /// <summary>
        /// Tests that the constructor verifies dependencies.
        /// </summary>
        [Fact]
        public void ConstructorShouldVerifyDependencies()
        {
            // arrange
            // act
            _ = new Seeder(fakes.IDependencyFactory.Object);

            // assert
            fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<IEntitySeeder<App>>(), Times.Once);

            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(1));
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<It.IsAnyType>(), Times.Exactly(1));
        }

        /// <summary>
        /// Tests that the seeder enabled property uses the value from the generation options.
        /// </summary>
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SeederEnabledShouldUseValueFromGenerationOptions(bool shouldSeed)
        {
            // arrange
            fakes.GenerationOptions.Setup(x => x.Seed).Returns(shouldSeed);
            Seeder seeder = new(fakes.IDependencyFactory.Object);


            // act
            bool enabled = seeder.Enabled;

            // assert
            Assert.Equal(shouldSeed, enabled);
        }

        /// <summary>
        /// Tests that the seeder enabled property uses the value from the generation options.
        /// </summary>
        [Fact]
        public void ExecuteShouldEnsureResettingAndSeedingShouldBeCalledInSequencedOrder()
        {
            // arrange
            int order = 0;
            
            Mock<IEntitySeeder<App>> entitySeeder1 = new();
            Mock<IEntitySeeder<App>> entitySeeder2 = new();
            Mock<IEntitySeeder<App>> entitySeeder3 = new();

            entitySeeder3.Setup(x => x.ResetOrder).Returns(1);
            entitySeeder3.Setup(x => x.Reset()) .Callback(() => Assert.Equal(1, ++order));

            entitySeeder2.Setup(x => x.ResetOrder).Returns(2);
            entitySeeder2.Setup(x => x.Reset()).Callback(() => Assert.Equal(2, ++order));

            entitySeeder1.Setup(x => x.ResetOrder).Returns(3);
            entitySeeder1.Setup(x => x.Reset()).Callback(() => Assert.Equal(3, ++order));

            entitySeeder1.Setup(x => x.SeedOrder).Returns(1);
            entitySeeder1.Setup(x => x.Seed(It.IsAny<App>())).Callback(() => Assert.Equal(4, ++order));

            entitySeeder2.Setup(x => x.SeedOrder).Returns(2);
            entitySeeder2.Setup(x => x.Seed(It.IsAny<App>())).Callback(() => Assert.Equal(5, ++order));

            entitySeeder3.Setup(x => x.SeedOrder).Returns(3);
            entitySeeder3.Setup(x => x.Seed(It.IsAny<App>())).Callback(() => Assert.Equal(6, ++order));

            fakes.IDependencyFactory.Setup(x => x.ResolveAll<IEntitySeeder<App>>()).Returns([entitySeeder1.Object, entitySeeder2.Object, entitySeeder3.Object]);

            Seeder seeder = new(fakes.IDependencyFactory.Object);

            // act
            seeder.Execute();

            // assert
            entitySeeder1.Verify(x => x.Reset(), Times.Once);
            entitySeeder2.Verify(x => x.Reset(), Times.Once);
            entitySeeder3.Verify(x => x.Reset(), Times.Once);

            entitySeeder1.Verify(x => x.Seed(It.IsAny<App>()), Times.Once);
            entitySeeder2.Verify(x => x.Seed(It.IsAny<App>()), Times.Once);
            entitySeeder3.Verify(x => x.Seed(It.IsAny<App>()), Times.Once);
        }
    }
}
