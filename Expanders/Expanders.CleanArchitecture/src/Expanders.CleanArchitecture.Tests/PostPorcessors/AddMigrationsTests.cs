using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.PostProcessors;
using LiquidVisions.PanthaRhei.Domain.Entities;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests.PostPorcessors
{
    /// <summary>
    /// Tests for <seealso cref="AddMigrations"/>.
    /// </summary>
    public class AddMigrationsTests
    {
        private readonly CleanArchitectureFakes fakes = new ();
        private readonly AddMigrations interactor;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddMigrationsTests"/> class.
        /// </summary>
        public AddMigrationsTests()
        {
            fakes.MockCleanArchitectureExpander();
            interactor = new (fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Tests for <seealso cref="AddMigrations.Enabled"/>
        /// </summary>
        /// <param name="modes"><seealso cref="GenerationModes"/>.</param>
        /// <param name="canExecuteResult">expected result.</param>
        [Theory]
        [InlineData(GenerationModes.None, false)]
        [InlineData(GenerationModes.Default, false)]
        [InlineData(GenerationModes.Extend, false)]
        [InlineData(GenerationModes.Deploy, false)]
        [InlineData(GenerationModes.Migrate, true)]
        [InlineData(GenerationModes.Harvest, false)]
        [InlineData(GenerationModes.Rejuvenate, false)]
        [InlineData(GenerationModes.Run, false)]
        [InlineData(GenerationModes.Migrate | GenerationModes.Default, true)]
        public void CanExecute_ShouldBeTrue(GenerationModes modes, bool canExecuteResult)
        {
            // arranges
            fakes.GenerationOptions.Setup(x => x.Modes).Returns(modes);

            // act
            bool result = interactor.Enabled;

            // assert
            fakes.GenerationOptions.Verify(x => x.Modes, Times.Once);
            Assert.Equal(canExecuteResult, result);
        }

        /// <summary>
        /// Tests for <seealso cref="AddMigrationsTests.Execute()"/>.
        /// </summary>
        [Fact]
        public void Execute()
        {
            // arrange
            // act
            interactor.Execute();

            // assert
            fakes.ICommandLine.Verify(x => x.Start(It.Is<string>(s => s.StartsWith("dotnet ef migrations add ")), "C:\\Some\\Component\\Output\\Path"), Times.Once);
        }
    }
}
