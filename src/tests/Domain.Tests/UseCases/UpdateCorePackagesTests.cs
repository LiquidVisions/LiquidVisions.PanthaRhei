using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Domain.Usecases.UpdateCoreUseCase;
using LiquidVisions.PanthaRhei.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Domain.Tests.UseCases
{
    /// <summary>
    /// Tests for <see cref="UpdateCorePackages"/>.
    /// </summary>
    public class UpdateCorePackagesTests
    {
        private readonly Fakes fakes = new();
        private readonly UpdateCorePackages useCase;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UpdateCorePackagesTests()
        {
            useCase = new UpdateCorePackages(
                fakes.IDirectory.Object,
                fakes.ICommandLine.Object,
                fakes.ILogger.Object,
                fakes.IFile.Object);
        }

        /// <summary>
        /// Tests the happy flow of <seealso cref="UpdateCorePackages.Execute(string)" />
        /// </summary>
        /// <param name="projectFile">The name of the project file</param>
        /// <param name="package">The name of the package</param>
        [Theory]
        [InlineData("project1.csproj", "LiquidVisions.PanthaRhei.Core")]
        [InlineData("project1.tests.csproj", "LiquidVisions.PanthaRhei.Tests")]
        [InlineData("project1.Tests.csproj", "LiquidVisions.PanthaRhei.Tests")]
        public async Task TestAppyFlowOnUpdatePackagesAndBuildSolutions([NotNull]string projectFile, [NotNull]string package)
        {
            // arrange
            string expectedRoot = "C:\\test";
            string expectedProjectDirectory = "C:\\test\\project1";
            string[] projectFiles = [$"C:\\test\\project1\\{projectFile}"];
            string expectedSolutionFile = "C:\\test\\solutionFile.sln";
            string projectFileWithoutExtension = projectFile.Replace(".csproj", string.Empty, StringComparison.OrdinalIgnoreCase);
            fakes.IFile.Setup(x => x.GetFileNameWithoutExtension(projectFiles[0])).Returns(projectFileWithoutExtension);
            fakes.IFile.Setup(x => x.GetDirectory(expectedSolutionFile)).Returns(expectedRoot);
            fakes.IDirectory.Setup(x => x.GetFiles(expectedRoot, "*.csproj", SearchOption.AllDirectories)).Returns(projectFiles);
            fakes.IDirectory.Setup(x => x.GetFiles(expectedRoot, "*.sln", SearchOption.AllDirectories)).Returns([expectedSolutionFile]);    
            fakes.IFile.Setup(x => x.GetDirectory(projectFiles[0])).Returns(expectedProjectDirectory);

            // act
            Response response = await useCase.Execute(expectedRoot);

            // assert
            Assert.NotNull(response);
            Assert.True(response.IsValid);
            Assert.Empty(response.Errors);

            fakes.IDirectory.Verify(x => x.GetFiles(expectedRoot, "*.csproj", SearchOption.AllDirectories), Times.Once);
            fakes.IDirectory.Verify(x => x.GetFiles(expectedRoot, "*.sln", SearchOption.AllDirectories), Times.Once);
            fakes.IDirectory.VerifyNoOtherCalls();

            fakes.IFile.Verify(x => x.GetDirectory(projectFiles[0]), Times.Once);
            fakes.IFile.Verify(x => x.GetFileNameWithoutExtension(projectFiles[0]), Times.Once);
            fakes.IFile.Verify(x => x.GetDirectory(expectedSolutionFile), Times.Once);
            fakes.IFile.VerifyNoOtherCalls();

            fakes.ILogger.Verify(x => x.Info($"Building solution file {expectedSolutionFile} to apply latest package update."), Times.Once);
            fakes.ILogger.Verify(x => x.Info($"Updating package {package} to latest version on {projectFileWithoutExtension}.csproj"), Times.Once);
            fakes.ILogger.VerifyNoOtherCalls();

            fakes.ICommandLine.Verify(x => x.Start("dotnet build", expectedRoot), Times.Once);
            fakes.ICommandLine.Verify(x => x.Start($"dotnet add package {package}", expectedProjectDirectory, true), Times.Once);
            fakes.ICommandLine.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Tests the case where an exception has been thrown.
        /// </summary>
        [Fact]
        public async Task ExceptionHasBeenThrownShouldReturnResponseFaultMessage()
        {
            // arrange
            Exception exception = new InvalidOperationException("Some error message");
            fakes.IDirectory.Setup(x => x.GetFiles(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<SearchOption>())).Throws(exception);

            // act
            Response response = await useCase.Execute("C:\\test");

            // assert
            Assert.NotNull(response);
            Assert.False(response.IsValid);
            Assert.Single(response.Errors);
            Assert.Equal(FaultCodes.InternalServerError, response.Errors.Single().FaultCode);
            Assert.Equal($"An unexpected error occurred: {exception.Message}", response.Errors.Single().FaultMessage);
        }
    }


}
