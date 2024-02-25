using System.IO;
using LiquidVisions.PanthaRhei.Application.Usecases.Templates;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Usecases.Templates
{
    /// <summary>
    /// Tests for <see cref="ScribanTemplate"/>.
    /// </summary>
    public class ScribanTemplateTests
    {
        private readonly ApplicationFakes fakes = new();
        private readonly ScribanTemplate scribanTemplate;
        private readonly Mock<ITemplateLoader> mockedTemplateLoader = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ScribanTemplateTests"/> class.
        /// </summary>
        public ScribanTemplateTests()
        {
            fakes.IDependencyFactory.Setup(x => x.Resolve<Scriban.Runtime.ScriptObject>()).Returns([]);
            fakes.IDependencyFactory.Setup(x => x.Resolve<ITemplateLoader>()).Returns(mockedTemplateLoader.Object);
            scribanTemplate = new(fakes.IDependencyFactory.Object);
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
            fakes.IDependencyFactory.Verify(x => x.Resolve<ILogger>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<Scriban.Runtime.ScriptObject>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<ITemplateLoader>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IFile>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IDirectory>(), Times.Once);
            fakes.IDependencyFactory.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Test that verifies that the template is rendered.
        /// </summary>
        [Fact]
        public void RenderShouldLoadTemplate()
        {
            // arrange
            string path = "C:\\Path\\To\\Template.template";
            object model = new();

            // act
            string result = scribanTemplate.Render(path, model);

            // assert
            mockedTemplateLoader.Verify(x => x.Load(path), Times.Once);
        }

        /// <summary>
        /// Test that verifies that the template is rendered and saved.
        /// </summary>
        /// <param name="folderExists"></param>
        /// <param name="times"></param>
        [Theory]
        [InlineData(true, 0)]
        [InlineData(false, 1)]
        public void RenderAndSaveShouldLoadTemplateAndSaveTheResult(bool folderExists, int times)
        {
            // arrange
            string pathToTemplate = "C:\\Path\\To\\Template.template";
            string outputDirectory = "C:\\Path\\To";
            string file = "Output.txt";
            string fullPathToOutput = Path.Combine(outputDirectory, file);
            fakes.IDirectory.Setup(x => x.Exists(outputDirectory)).Returns(folderExists);
            fakes.IFile.Setup(x => x.GetDirectory(fullPathToOutput)).Returns(outputDirectory);

            object model = new();

            // act
            scribanTemplate.RenderAndSave(pathToTemplate, model, fullPathToOutput);

            // assert
            fakes.IFile.Verify(x => x.GetDirectory(fullPathToOutput), Times.Once);
            fakes.IDirectory.Verify(x => x.Exists(outputDirectory), Times.Once);
            fakes.IDirectory.Verify(x => x.Create(outputDirectory), Times.Exactly(times));
            mockedTemplateLoader.Verify(x => x.Load(pathToTemplate), Times.Once);
            fakes.IFile.Verify(x => x.WriteAllText(fullPathToOutput, It.IsAny<string>()), Times.Once);
        }
    }
}
