using System;
using System.IO;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Tests.Mocks;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.PostProcessors;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Preprocessors;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Rejuvenators;
using LiquidVisions.PanthaRhei.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Domain.Tests.UseCases.Generators
{
    /// <summary>
    /// Tests the <see cref="UnInstallDotNetTemplate{TExpander}"/> class.
    /// </summary>
    public class UnInstallDotNetTemplateTests
    {
        private readonly Fakes _fakes = new();
        private readonly UnInstallDotNetTemplate<FakeExpander> _processor;
        private readonly Mock<FakeExpander> _expander = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="UnInstallDotNetTemplateTests"/> class.
        /// </summary>
        public UnInstallDotNetTemplateTests()
        {
            _expander.Setup(expander => expander.Model.Name).Returns("FakeExpander");
            _fakes.IDependencyFactory.Setup(x => x.Resolve<FakeExpander>()).Returns(_expander.Object);
            _processor = new UnInstallDotNetTemplate<FakeExpander>(_fakes.IDependencyFactory.Object);
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
            _fakes.IDependencyFactory.Verify(x => x.Resolve<App>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<ICommandLine>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<IDirectory>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<ILogger>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<FakeExpander>(), Times.Once);

            _fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(6));
        }

        /// <summary>
        /// Tests ArgumentNullException in Object Constructor.
        /// </summary>
        [Fact]
        public void AssertThrowsArgumentNullException()
        {
            // arrange

            // act
            // assert
            Assert.Throws<ArgumentNullException>(() => new InstallDotNetTemplate<FakeExpander>(null));
        }

        /// <summary>
        /// Tests for <see cref="InstallDotNetTemplate{TExpander}"/> Expander property.
        /// </summary>
        [Fact]
        public void ExpanderPropertyShouldReturnResolvedExpander()
        {
            // arrange

            InstallDotNetTemplate<FakeExpander> processor = new(_fakes.IDependencyFactory.Object);

            // act
            // assert
            Assert.Equal(_expander.Object, processor.Expander);
        }

        /// <summary>
        /// Tests for <see cref="RegionRejuvenator{TExpander}.Enabled"/>.
        /// </summary>
        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void ShouldVerifyEnabledProperty(bool clean, bool expectedResult)
        {
            // arrange
            _fakes.GenerationOptions.Setup(x => x.Clean).Returns(clean);

            // act
            // assert
            Assert.Equal(expectedResult, _processor.Enabled);
        }

        /// <summary>
        /// Test for <see cref="UnInstallDotNetTemplate{TExpander}.Execute"/>.
        /// </summary>
        [Fact]
        public void UnInstallTemplate()
        {
            // arrange
            string expectedPath = "C:/Random/Path/";

            string expectedTemplatePath = Path.Combine(_fakes.GenerationOptions.Object.ExpandersFolder, _expander.Object.Model.Name, Resources.TemplatesFolder);
            _fakes.IDirectory.Setup(x => x.Exists(expectedTemplatePath)).Returns(true);
            _fakes.IDirectory.Setup(x => x.GetDirectories(expectedTemplatePath, ".template.config", SearchOption.AllDirectories)).Returns([$"{expectedPath}.template.config"]);
            _fakes.IDirectory.Setup(x => x.GetNameOfParentDirectory($"{expectedPath}.template.config")).Returns(expectedPath);

            // act
            _processor.Execute();

            // assert
            _fakes.IDirectory.Verify(x => x.GetDirectories(expectedTemplatePath, ".template.config", SearchOption.AllDirectories), Times.Once);
            _fakes.IDirectory.Verify(x => x.GetNameOfParentDirectory($"{expectedPath}.template.config"), Times.Once);
            _fakes.ICommandLine.Verify(x => x.Start($"dotnet new uninstall {expectedPath}"), Times.Once);
            _fakes.ILogger.Verify(x => x.Info($"Uninstalling template from location {expectedPath}"), Times.Once);
        }

    }
}
