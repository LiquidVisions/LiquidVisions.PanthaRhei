using LiquidVisions.PanthaRhei.Application.Usecases.Templates;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Usecases.Templates
{
    /// <summary>
    /// Tests for <see cref="TemplateLoader"/>.
    /// </summary>
    public class TemplateLoaderTests
    {
        private readonly ApplicationFakes _fakes = new();
        private readonly TemplateLoader _templateLoader;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateLoaderTests"/> class.
        /// </summary>
        public TemplateLoaderTests()
        {
            _templateLoader = new(_fakes.IDependencyFactory.Object);
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
            _fakes.IDependencyFactory.Verify(x => x.Resolve<IFile>(), Times.Once);
            _fakes.IDependencyFactory.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Test that verifies that the template is loaded.
        /// </summary>
        [Fact]
        public void ShouldLoadTemplate()
        {
            // arrange
            string path = "C:\\Path\\To\\Template.template";
            _fakes.IFile.Setup(x => x.Exists(path)).Returns(true);

            // act
            _templateLoader.Load(path);

            // assert
            _fakes.IFile.Verify(x => x.Exists(path), Times.Once);
            _fakes.IFile.Verify(x => x.ReadAllText(path), Times.Once);
            _fakes.ILogger.Verify(x => x.Info($"Loading template on path '{path}'"), Times.Once);
        }

        /// <summary>
        /// Test that verifies that a TemplateException is thrown when the template does not exist.
        /// </summary>
        [Fact]
        public void VerifiesThatTemplateExceptionIsThrownWhenTemplateDoesNotExist()
        {
            // arrange
            string path = "C:\\Path\\To\\Template.template";

            // act
            // assert
            TemplateException exception = Assert.Throws<TemplateException>(() => _templateLoader.Load(path));
            Assert.Equal($"Failed to load template '{path}'", exception.Message);
        }
    }
}
