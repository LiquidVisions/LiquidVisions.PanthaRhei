using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.CreateNewExpander;
using LiquidVisions.PanthaRhei.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Domain.Tests.UseCases
{
    /// <summary>
    /// Tests for <seealso cref="CreateNewExpander"/>.
    /// </summary>
    public class NewExpanderUserCaseTests
    {
        private readonly CreateNewExpander useCase;
        private readonly Fakes fakes = new();
        private readonly CreateNewExpanderRequestModel model;

        private string sourceTemplateFile;
        private string sourceTemplateDirectory;

        /// <summary>
        /// default constructor
        /// </summary>
        public NewExpanderUserCaseTests()
        {
            useCase = new CreateNewExpander(fakes.ICommandLine.Object, fakes.IDirectory.Object, fakes.IFile.Object, fakes.ILogger.Object);
            model = new()
            {
                Build = true,
                ShortName = "ShortName",
                FullName = "FullName.ShortName",
                Path = "C:\\Path",
                BuildPath = "C:\\BuildPath"
            };

            
        }

        private void MockIOOperations(int numberOfReturnedFiles = 1, int numberOfReturnedDirectories = 1)
        {
            sourceTemplateDirectory = Path.Combine(model.Path, "_template.config");
            sourceTemplateFile = Path.Combine(model.Path, sourceTemplateDirectory, ".template.json");

            string[] fileResult = Enumerable.Repeat(sourceTemplateFile, numberOfReturnedFiles).ToArray();
            string[] directoryResult = Enumerable.Repeat(sourceTemplateDirectory, numberOfReturnedDirectories).ToArray();
            
            fakes.IDirectory.Setup(x => x.GetFiles(model.Path, ".template.json", SearchOption.AllDirectories))
                .Returns(fileResult);

            fakes.IDirectory.Setup(x => x.GetDirectories(model.Path, "_template.config", SearchOption.AllDirectories))
                .Returns(directoryResult);
        }

        /// <summary>
        /// Should execute build when required
        /// </summary>
        /// <param name="build"></param>
        /// <param name="times"></param>
        [Theory]
        [InlineData(true, 1)]
        [InlineData(false, 0)]
        public void ShouldExecuteBuildWhenRequired(bool build, int times)
        {
            // arrange
            model.Build = build;
            MockIOOperations();

            // act
            useCase.Execute(model);

            // assert
            fakes.ICommandLine.Verify(x => x.Start("dotnet build", Path.Combine(model.Path, model.FullName)), Times.Exactly(times));
        }

        /// <summary>
        /// Tests the happy flow of the use case
        /// </summary>
        [Fact]
        public void HappyFlowShouldVerifyAll()
        {
            // arrange
            MockIOOperations();

            // act
            useCase.Execute(model);

            // assert
            fakes.ICommandLine.Verify(x => x.Start("dotnet new install LiquidVisions.PanthaRhei.Templates.Expander --force", true), Times.Once());
            fakes.ICommandLine.Verify(x => x.Start($"dotnet new expander -n {model.FullName} --buildPath {model.BuildPath} --shortName {model.ShortName} -d", model.Path, true), Times.Once());
            fakes.ICommandLine.Verify(x => x.Start("dotnet build", Path.Combine(model.Path, model.FullName)), Times.Once());
            fakes.ICommandLine.Verify(x => x.Start("dotnet new uninstall LiquidVisions.PanthaRhei.Templates.Expander", true), Times.Once());
            fakes.ICommandLine.Verify(x => x.Start(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Exactly(1));
            fakes.ICommandLine.Verify(x => x.Start(It.IsAny<string>(), It.IsAny<bool>()), Times.Exactly(2));
            fakes.ICommandLine.Verify(x => x.Start(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));
            fakes.IDirectory.Verify(x => x.GetFiles(model.Path, ".template.json", SearchOption.AllDirectories), Times.Once());
            fakes.IDirectory.Verify(x => x.GetDirectories(model.Path, "_template.config", SearchOption.AllDirectories), Times.Once());
            fakes.IDirectory.Verify(x => x.Rename(sourceTemplateDirectory, sourceTemplateDirectory.Replace("_template.config", ".template.config")), Times.Once());
            fakes.IFile.Verify(x => x.Rename(sourceTemplateFile, sourceTemplateFile.Replace(".template.json", "template.json")), Times.Once());
            fakes.ICommandLine.VerifyNoOtherCalls();
            fakes.IDirectory.VerifyNoOtherCalls();
            fakes.IFile.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Should throw an exception when no or more than 1 template file are found
        /// </summary>
        /// <param name="numberOfFilesFound"></param>
        /// <param name="numberOfFoldersFound"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        [Theory]
        [InlineData(2, 1, "file", ".template.json")]
        [InlineData(0, 1, "file", ".template.json")]
        [InlineData(1, 0, "directory", "_template.config")]
        [InlineData(1, 2, "directory", "_template.config")]
        public void ChecksWhetherOnlyASingleTemplateIsFoundByIOOperations(int numberOfFilesFound, int numberOfFoldersFound, string type, string name)
        {
            // arrange
            int totalFound = type == "directory" ? numberOfFoldersFound : numberOfFilesFound;
            string errorMessage = $"Expected to find one {type} named {name} but found {totalFound}.";
            MockIOOperations(numberOfFilesFound, numberOfFoldersFound);

            // act
            void action() => useCase.Execute(model);

            // assert
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(action);
            Assert.Equal(errorMessage, exception.Message);
        }
    }
}
