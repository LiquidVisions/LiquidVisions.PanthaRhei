using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.Application.Tests.Interactors.Generators
{
    public class CodeGeneratorInteractorTests
    {
        private readonly CodeGeneratorInteractor interactor;
        private readonly Fakes fakes = new();
        private readonly Mock<IExpanderInteractor> mockedIExpanderInteractor = new();

        public CodeGeneratorInteractorTests()
        {
            mockedIExpanderInteractor.Setup(x => x.Model).Returns(new Expander());

            fakes.IDependencyFactoryInteractor
                .Setup(x => x.GetAll<IExpanderInteractor>())
                .Returns(new List<IExpanderInteractor> { mockedIExpanderInteractor.Object });

            interactor = new CodeGeneratorInteractor(fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Constructor_Dependencies_ShouldVeryfy()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<Parameters>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IDirectory>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(2));

            fakes.IDependencyFactoryInteractor.Verify(x => x.GetAll<IExpanderInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.GetAll<It.IsAnyType>(), Times.Exactly(1));
        }

        [Theory]
        [InlineData(true, 1)]
        [InlineData(false, 0)]
        public void Clean_ShouldRunExactTimes(bool clean, int times)
        {
            // arrange
            fakes.Parameters.Setup(x => x.Clean)
                .Returns(clean);

            // act
            interactor.Execute();

            // assert
            fakes.Parameters.Verify(x => x.Clean, Times.Once);
            fakes.Parameters.Verify(x => x.OutputFolder, Times.Exactly(times));
            fakes.IDirectory.Verify(x => x.Delete(fakes.Parameters.Object.OutputFolder), Times.Exactly(times));
        }

        [Fact]
        public void Execute_HappyFlow_ShouldVerify()
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
