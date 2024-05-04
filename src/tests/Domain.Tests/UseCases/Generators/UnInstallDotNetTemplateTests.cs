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
        private readonly Fakes fakes = new();
        private readonly UnInstallDotNetTemplate<FakeExpander> processor;
        private readonly Mock<FakeExpander> expander = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="UnInstallDotNetTemplateTests"/> class.
        /// </summary>
        public UnInstallDotNetTemplateTests()
        {
            expander.Setup(expander => expander.Model.Name).Returns("FakeExpander");
            fakes.IDependencyFactory.Setup(x => x.Resolve<FakeExpander>()).Returns(expander.Object);
            processor = new UnInstallDotNetTemplate<FakeExpander>(fakes.IDependencyFactory.Object);
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
            fakes.IDependencyFactory.Verify(x => x.Resolve<App>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<ICommandLine>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IDirectory>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<ILogger>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<FakeExpander>(), Times.Once);

            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(6));
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

            InstallDotNetTemplate<FakeExpander> processor = new(fakes.IDependencyFactory.Object);

            // act
            // assert
            Assert.Equal(expander.Object, processor.Expander);
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
            fakes.GenerationOptions.Setup(x => x.Clean).Returns(clean);

            // act
            // assert
            Assert.Equal(expectedResult, processor.Enabled);
        }

        /// <summary>
        /// Test for <see cref="UnInstallDotNetTemplate{TExpander}.Execute"/>.
        /// </summary>
        [Fact]
        public void UnInstallTemplate()
        {
            // arrange
            string expectedPath = "C:/Random/Path/";

            string expectedTemplatePath = Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, expander.Object.Model.Name, Resources.TemplatesFolder);
            fakes.IDirectory.Setup(x => x.Exists(expectedTemplatePath)).Returns(true);
            fakes.IDirectory.Setup(x => x.GetDirectories(expectedTemplatePath, ".template.config", SearchOption.AllDirectories)).Returns([$"{expectedPath}.template.config"]);
            fakes.IDirectory.Setup(x => x.GetNameOfParentDirectory($"{expectedPath}.template.config")).Returns(expectedPath);

            // act
            processor.Execute();

            // assert
            fakes.IDirectory.Verify(x => x.GetDirectories(expectedTemplatePath, ".template.config", SearchOption.AllDirectories), Times.Once);
            fakes.IDirectory.Verify(x => x.GetNameOfParentDirectory($"{expectedPath}.template.config"), Times.Once);
            fakes.ICommandLine.Verify(x => x.Start($"dotnet new uninstall {expectedPath}"), Times.Once);
            fakes.ILogger.Verify(x => x.Info($"Uninstalling template from location {expectedPath}"), Times.Once);
        }

    }
}
