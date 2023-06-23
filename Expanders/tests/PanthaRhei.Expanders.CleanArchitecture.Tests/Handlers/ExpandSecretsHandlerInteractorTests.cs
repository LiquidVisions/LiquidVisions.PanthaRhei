using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.CleanArchitecture.Tests.Handlers
{
    public class ExpandSecretsHandlerInteractorTests
    {
        private readonly CleanArchitectureFakes fakes = new();
        private readonly ExpandSecretsHandlerInteractor interactor;

        public ExpandSecretsHandlerInteractorTests()
        {
            App app = fakes.SetupApp();
            fakes.CleanArchitectureExpander.Setup(x => x.App).Returns(app);
            interactor = new ExpandSecretsHandlerInteractor(fakes.CleanArchitectureExpander.Object, fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Dependencies_ShouldBeResolved()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ICommandLineInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<App>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(3));
        }

        [Fact]
        public void Order_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(16, interactor.Order);
        }

        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            string name = interactor.Name;

            // assert
            Assert.Equal(nameof(ExpandSecretsHandlerInteractor), name);
        }

        [Theory]
        [InlineData(GenerationModes.Default, true)]
        [InlineData(GenerationModes.Migrate, false)]
        [InlineData(GenerationModes.Extend, true)]
        [InlineData(GenerationModes.Deploy, false)]
        [InlineData(GenerationModes.None, false)]
        public void CanExecute_ShouldBeFalse(GenerationModes mode, bool expectedResult)
        {
            // arrange
            fakes.GenerationOptions.Setup(x => x.Modes).Returns(mode);

            // act
            // assert
            Assert.Equal(expectedResult, interactor.CanExecute);
        }

        [Fact]
        public void Execute_ShouldExecuteCommand()
        {
            // arrange
            string path1 = "C:\\Path1";
            string path2 = "C:\\Path2";
            fakes.CleanArchitectureExpander.Setup(x => x.GetComponentPaths(Expanders.CleanArchitecture.Resources.Api, Expanders.CleanArchitecture.Resources.EntityFramework)).Returns(new List<string>() { path1, path2 });

            // act
            interactor.Execute();

            // assert
            fakes.ICommandLineInteractor.Verify(x => x.Start("dotnet user-secrets init", path1), Times.Once);
            fakes.ICommandLineInteractor.Verify(x => x.Start("dotnet user-secrets init", path2), Times.Once);
            fakes.ICommandLineInteractor.Verify(x => x.Start($"dotnet user-secrets set \"ConnectionStrings:DefaultConnectionString\" \"SomeConnectionStringDefinition\"", path1), Times.Once);
            fakes.ICommandLineInteractor.Verify(x => x.Start($"dotnet user-secrets set \"ConnectionStrings:DefaultConnectionString\" \"SomeConnectionStringDefinition\"", path2), Times.Once);
        }
    }
}
