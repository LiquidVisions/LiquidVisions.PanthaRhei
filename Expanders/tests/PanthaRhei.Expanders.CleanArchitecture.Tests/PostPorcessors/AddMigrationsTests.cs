using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.PostProcessors;
using LiquidVisions.PanthaRhei.Domain.Entities;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests.PostPorcessors
{
    public class AddMigrationsTests
    {
        private readonly CleanArchitectureFakes fakes = new();
        private readonly AddMigrations interactor;

        public AddMigrationsTests()
        {
            fakes.MockCleanArchitectureExpander();
            interactor = new(fakes.IDependencyFactoryInteractor.Object);
        }

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

        [Fact]
        public void Execute()
        {
            // arrange
            // act
            interactor.Execute();

            // assert
            fakes.ICommandLineInteractor.Verify(x => x.Start(It.Is<string>(s => s.StartsWith("dotnet ef migrations add ")), "C:\\Some\\Component\\Output\\Path"), Times.Once);
        }
    }
}
