using System;
using System.IO;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Domain.Tests.UseCases
{
    /// <summary>
    /// Tests the <see cref="DotNetApplication"/> class.
    /// </summary>
    public class DotNetApplicationTests
    {
        private readonly Fakes fakes = new();
        private readonly DotNetApplication application;
        private readonly Mock<App> mockedApp = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="DotNetApplicationTests"/> class.
        /// </summary>
        public DotNetApplicationTests()
        {
            fakes.IDependencyFactory.Setup(x => x.Resolve<App>()).Returns(mockedApp.Object);
            mockedApp.Setup(x => x.FullName).Returns("LiquidVisions.PanthaRhei.Tests");
            application = new DotNetApplication(fakes.IDependencyFactory.Object);
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
            fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<ILogger>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IDirectory>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IFile>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<App>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<ICommandLine>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(6));
        }

        /// <summary>
        /// Tests for <seealso cref="DotNetApplication.AddPackages(Component)"/>
        /// </summary>
        [Fact]
        public void AddPackageThrowsNotImplementedException()
        {
            // arrange
            // act
            // assert
            Assert.Throws<NotImplementedException>(() => application.AddPackages(new Component()));
        }

        /// <summary>
        /// Tests for <seealso cref="DotNetApplication.MaterializeProject"/>
        /// </summary>
        /// <param name="folderExists"></param>
        /// <param name="times"></param>
        [Theory]
        [InlineData(false, 1)]
        [InlineData(true, 0)]
        public void MaterializeProjectShouldExecuteDotNetNewCommand(bool folderExists, int times)
        {
            // arrange
            string expectedSolutionFolder = Path.Combine(fakes.GenerationOptions.Object.OutputFolder, mockedApp.Object.FullName);
            fakes.IDependencyFactory.Setup(x => x.Resolve<IDirectory>().Exists(expectedSolutionFolder)).Returns(folderExists);

            // act
            application.MaterializeProject();

            // assert
            fakes.ILogger.Verify(x => x.Info($"Creating directory {expectedSolutionFolder}"), Times.Exactly(times));
            fakes.ICommandLine.Verify(x => x.Start($"mkdir {expectedSolutionFolder}"), Times.Exactly(times));
            fakes.ICommandLine.Verify(x => x.Start($"dotnet new liquidvisions-expanders-{mockedApp.Object.Name} --NAME {mockedApp.Object.Name} --NS {mockedApp.Object.FullName}", expectedSolutionFolder), Times.Once);
        }

        /// <summary>
        /// Tests for <seealso cref="DotNetApplication.MaterializeComponent(Component)"/>
        /// </summary>
        /// <param name="folderExists">Indicating whether the root folder exists</param>
        /// <param name="solutionFileExists">Indicating whether the solution file exists</param>
        /// <param name="timesToCreateFolder">Total times the root folder is created</param>
        /// <param name="timesToCreateSolutionFile">Total times the solution file is created.</param>
        [Theory]
        [InlineData(false, false,1, 1)]
        [InlineData(true, true, 0, 0)]
        public void MaterializeComponentComponentShouldBeCreatedByDotNetCommand(bool folderExists, bool solutionFileExists, int timesToCreateFolder, int timesToCreateSolutionFile)
        {
            // arrange
            Mock<Component> mockedComponent = new();
            mockedComponent.Setup(x => x.Name).Returns("Component");

            string expectedSolutionFolder = Path.Combine(fakes.GenerationOptions.Object.OutputFolder, mockedApp.Object.FullName);
            string expectedComponentFolder = Path.Combine(expectedSolutionFolder, "src", mockedComponent.Object.Name);
            string expectedComponentConfigurationFile = Path.Combine(expectedComponentFolder, $"{mockedComponent.Object.Name}.csproj");
            string expectedSolutionConfigurationFile = Path.Combine(expectedSolutionFolder, $"{mockedApp.Object.FullName}.sln");

            fakes.IDependencyFactory.Setup(x => x.Resolve<IDirectory>().Exists(expectedSolutionFolder)).Returns(folderExists);
            fakes.IDependencyFactory.Setup(x => x.Resolve<IDirectory>().Exists(expectedComponentFolder)).Returns(folderExists);
            fakes.IDependencyFactory.Setup(x => x.Resolve<IFile>().Exists(expectedSolutionConfigurationFile)).Returns(solutionFileExists);

            // act
            application.MaterializeComponent(mockedComponent.Object);

            // assert
            fakes.ILogger.Verify(x => x.Info($"Creating directory {expectedSolutionFolder}"), Times.Exactly(timesToCreateFolder));
            fakes.ICommandLine.Verify(x => x.Start($"mkdir {expectedSolutionFolder}"), Times.Exactly(timesToCreateFolder));
            fakes.ILogger.Verify(x => x.Info($"Creating directory {expectedComponentFolder}"), Times.Exactly(timesToCreateFolder));
            fakes.ICommandLine.Verify(x => x.Start($"mkdir {expectedComponentFolder}"), Times.Exactly(timesToCreateFolder));
            fakes.IFile.Verify(x => x.Exists(expectedSolutionConfigurationFile), Times.Once);
            fakes.ICommandLine.Verify(x => x.Start($"dotnet new sln", expectedSolutionFolder), Times.Exactly(timesToCreateSolutionFile));
            fakes.ICommandLine.Verify(x => x.Start($"dotnet new liquidvisions-expanders-{mockedComponent.Object.Name} --NAME {mockedComponent.Object.Name} --NS {mockedApp.Object.FullName}", expectedComponentFolder), Times.Once);
            fakes.ICommandLine.Verify(x => x.Start($"dotnet sln {expectedSolutionConfigurationFile} add {expectedComponentConfigurationFile}"), Times.Once);

        }
    }
}
