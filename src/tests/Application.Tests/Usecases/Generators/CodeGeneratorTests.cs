using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Application.Usecases.Generators;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;
using LiquidVisions.PanthaRhei.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Usecases.Generators
{
    /// <summary>
    /// Tests for <seealso cref="CodeGenerator"/>.
    /// </summary>
    public class CodeGeneratorTests
    {
        private readonly CodeGenerator interactor;
        private readonly Fakes fakes = new();
        private readonly Mock<IExpander> mockedIExpanderInteractor = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGeneratorTests"/> class.
        /// </summary>
        public CodeGeneratorTests()
        {
            mockedIExpanderInteractor.Setup(x => x.Model).Returns(new Expander());

            fakes.IDependencyFactory
                .Setup(x => x.ResolveAll<IExpander>())
                .Returns([mockedIExpanderInteractor.Object]);

            interactor = new CodeGenerator(fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Dependency tests.
        /// </summary>
        [Fact]
        public void ConstructorDependenciesShouldVerify()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IDirectory>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(2));

            fakes.IDependencyFactory.Verify(x => x.ResolveAll<IExpander>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<It.IsAnyType>(), Times.Exactly(1));
        }

        /// <summary>
        /// Tests whether the Clean option should be executed.
        /// </summary>
        /// <param name="clean">A boolean indicating the <see cref="GenerationOptions.Clean"/> value.</param>
        /// <param name="times">Total amount of times the various calls should be executed.</param>
        [Theory]
        [InlineData(true, 1)]
        [InlineData(false, 0)]
        public void CleanShouldRunExactTimes(bool clean, int times)
        {
            // arrange
            fakes.GenerationOptions.Setup(x => x.Clean)
                .Returns(clean);

            // act
            interactor.Execute();

            // assert
            fakes.GenerationOptions.Verify(x => x.Clean, Times.Once);
            fakes.GenerationOptions.Verify(x => x.OutputFolder, Times.Exactly(times));
            fakes.IDirectory.Verify(x => x.Delete(fakes.GenerationOptions.Object.OutputFolder), Times.Exactly(times));
        }

        /// <summary>
        /// Tests the happy flow of the <seealso cref="CodeGenerator.Execute"/>.
        /// </summary>
        [Fact]
        public void ExecuteHappyFlowShouldVerify()
        {
            // arrange
            // act
            interactor.Execute();

            // assert
            mockedIExpanderInteractor.Verify(x => x.Harvest(), Times.Once);
            mockedIExpanderInteractor.Verify(x => x.PreProcess(), Times.Once);
            mockedIExpanderInteractor.Verify(x => x.Expand(), Times.Once);
            mockedIExpanderInteractor.Verify(x => x.Rejuvenate(), Times.Once);
            mockedIExpanderInteractor.Verify(x => x.PostProcess(), Times.Once);
        }
    }
}
