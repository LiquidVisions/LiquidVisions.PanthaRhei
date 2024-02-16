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
        private readonly ApplicationFakes _fakes = new();
        private readonly ScribanTemplate _scribanTemplate;
        private readonly Mock<ITemplateLoader> _mockedTemplateLoader = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ScribanTemplateTests"/> class.
        /// </summary>
        public ScribanTemplateTests()
        {
            _fakes.IDependencyFactory.Setup(x => x.Resolve<Scriban.Runtime.ScriptObject>()).Returns([]);
            _fakes.IDependencyFactory.Setup(x => x.Resolve<ITemplateLoader>()).Returns(_mockedTemplateLoader.Object);
            _scribanTemplate = new(_fakes.IDependencyFactory.Object);
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
            _fakes.IDependencyFactory.Verify(x => x.Resolve<ILogger>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<Scriban.Runtime.ScriptObject>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<ITemplateLoader>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<IFile>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<IDirectory>(), Times.Once);
            _fakes.IDependencyFactory.VerifyNoOtherCalls();
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
            string result = _scribanTemplate.Render(path, model);

            // assert
            _mockedTemplateLoader.Verify(x => x.Load(path), Times.Once);
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
            _fakes.IDirectory.Setup(x => x.Exists(outputDirectory)).Returns(folderExists);
            _fakes.IFile.Setup(x => x.GetDirectory(fullPathToOutput)).Returns(outputDirectory);

            object model = new();

            // act
            _scribanTemplate.RenderAndSave(pathToTemplate, model, fullPathToOutput);

            // assert
            _fakes.IFile.Verify(x => x.GetDirectory(fullPathToOutput), Times.Once);
            _fakes.IDirectory.Verify(x => x.Exists(outputDirectory), Times.Once);
            _fakes.IDirectory.Verify(x => x.Create(outputDirectory), Times.Exactly(times));
            _mockedTemplateLoader.Verify(x => x.Load(pathToTemplate), Times.Once);
            _fakes.IFile.Verify(x => x.WriteAllText(fullPathToOutput, It.IsAny<string>()), Times.Once);
        }
    }
}
