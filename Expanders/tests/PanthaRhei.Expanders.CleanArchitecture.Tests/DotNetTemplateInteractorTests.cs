using System.IO;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Logging;
using Moq;
using Xunit;
using LiquidVisions.PanthaRhei.Domain.Usecases;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests
{
    public class DotNetTemplateInteractorTests
    {
        private readonly CleanArchitectureFakes fakes = new();
        private readonly DotNetTemplateInteractor interactor;

        public DotNetTemplateInteractorTests()
        {
            fakes.MockCleanArchitectureExpander();

            interactor = new(fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Dependencies_ShouldBeResolved()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ICommandLineInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ILogger>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<App>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<CleanArchitectureExpander>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(5));
        }

        [Fact]
        public void CreateNew_ShouldCreateDirectoryAndExecuteDotNetNewCommand()
        {
            // arrange
            string expectedCommandLineParameters = "CustomCommandLineParameters";
            string expectedOutputFolder = Path.Combine(fakes.GenerationOptions.Object.OutputFolder, CleanArchitectureFakes.DefaultAppFullName);

            // act
            interactor.CreateNew(expectedCommandLineParameters);

            // assert
            fakes.ILogger.Verify(x => x.Info($"Creating directory {expectedOutputFolder}"), Times.Once);
            fakes.ICommandLineInteractor.Verify(x => x.Start($"mkdir {expectedOutputFolder}"), Times.Once);

            fakes.ILogger.Verify(x => x.Info($"Creating {CleanArchitectureFakes.DefaultAppName} @ {expectedOutputFolder}"), Times.Once);
            fakes.ICommandLineInteractor.Verify(x => x.Start($"dotnet new {expectedCommandLineParameters} --NAME {CleanArchitectureFakes.DefaultAppName} --ns {CleanArchitectureFakes.DefaultAppFullName}", expectedOutputFolder), Times.Once);
        }

        [Fact]
        public void ApplyPackageOnComponent_ShouldStartDotNetAddCommand()
        {
            // arrange
            string expectedFullPathToProject = "C:\\Custom\\Path\\To\\Project.csproj";
            fakes.CleanArchitectureExpander.Setup(x => x.GetComponentProjectFile(It.IsAny<Component>())).Returns(expectedFullPathToProject);
            string expectedPackageName = "PackageName";
            string expectedPackageVersion = "1.0.0";
            Component component = new();
            Package package = new() { Name = expectedPackageName, Version = expectedPackageVersion };

            // act
            interactor.ApplyPackageOnComponent(component, package);

            // assert
            fakes.ILogger.Verify(x => x.Info($"Adding nuget package {package.Name} to {expectedFullPathToProject}"), Times.Once);
            fakes.ICommandLineInteractor.Verify(x => x.Start($"dotnet add \"{expectedFullPathToProject}\" package \"{expectedPackageName}\" --version {expectedPackageVersion} -n"), Times.Once);
        }
    }
}
