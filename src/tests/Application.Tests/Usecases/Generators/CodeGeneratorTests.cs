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
        private readonly CodeGenerator _interactor;
        private readonly Fakes _fakes = new();
        private readonly Mock<IExpander> _mockedIExpanderInteractor = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGeneratorTests"/> class.
        /// </summary>
        public CodeGeneratorTests()
        {
            _mockedIExpanderInteractor.Setup(x => x.Model).Returns(new Expander());

            _fakes.IDependencyFactory
                .Setup(x => x.ResolveAll<IExpander>())
                .Returns(new List<IExpander> { _mockedIExpanderInteractor.Object });

            _interactor = new CodeGenerator(_fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Dependency tests.
        /// </summary>
        [Fact]
        public void ConstructorDependenciesShouldVeryfy()
        {
            // arrange
            // act
            // assert
            _fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<IDirectory>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(2));

            _fakes.IDependencyFactory.Verify(x => x.ResolveAll<IExpander>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.ResolveAll<It.IsAnyType>(), Times.Exactly(1));
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
            _fakes.GenerationOptions.Setup(x => x.Clean)
                .Returns(clean);

            // act
            _interactor.Execute();

            // assert
            _fakes.GenerationOptions.Verify(x => x.Clean, Times.Once);
            _fakes.GenerationOptions.Verify(x => x.OutputFolder, Times.Exactly(times));
            _fakes.IDirectory.Verify(x => x.Delete(_fakes.GenerationOptions.Object.OutputFolder), Times.Exactly(times));
        }

        /// <summary>
        /// Tests the happy flow of the <seealso cref="CodeGenerator.Execute"/>.
        /// </summary>
        [Fact]
        public void ExecuteHappyFlowShouldVerify()
        {
            // arrange
            // act
            _interactor.Execute();

            // assert
            _mockedIExpanderInteractor.Verify(x => x.Harvest(), Times.Once);
            _mockedIExpanderInteractor.Verify(x => x.PreProcess(), Times.Once);
            _mockedIExpanderInteractor.Verify(x => x.Expand(), Times.Once);
            _mockedIExpanderInteractor.Verify(x => x.Rejuvenate(), Times.Once);
            _mockedIExpanderInteractor.Verify(x => x.PostProcess(), Times.Once);
        }
    }
}
