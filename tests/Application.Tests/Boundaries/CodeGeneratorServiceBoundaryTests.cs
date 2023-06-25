using System;
using LiquidVisions.PanthaRhei.Application.Boundaries;
using LiquidVisions.PanthaRhei.Application.Interactors.Generators;
using LiquidVisions.PanthaRhei.Application.Interactors.Seeders;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Boundaries
{
    public class CodeGeneratorServiceBoundaryTests
    {
        private readonly Fakes fakes = new();
        private readonly ExpandBoundary boundary;
        private readonly Mock<ISeederInteractor> mockedSeederInteractor = new();

        public CodeGeneratorServiceBoundaryTests()
        {
            fakes.IDependencyFactoryInteractor.Setup(x => x.Get<ISeederInteractor>()).Returns(mockedSeederInteractor.Object);
            fakes.ILogManager.Setup(x => x.GetExceptionLogger()).Returns(fakes.ILogger.Object);
            boundary = new ExpandBoundary(fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Constructor_ShouldResolve()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ICodeGeneratorBuilderInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ISeederInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ILogger>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ILogManager>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(5));
        }

        [Fact]
        public void Execute_ShouldBuildCodeGeneratorAndExecute()
        {
            // arrange
            mockedSeederInteractor.Setup(x => x.CanExecute).Returns(false);

            // act
            boundary.Execute();

            // assert
            fakes.ICodeGeneratorBuilderInteractor.Verify(x => x.Build(), Times.Once);
            fakes.ICodeGeneratorInteractor.Verify(x => x.Execute(), Times.Once);
        }

        [Fact]
        public void Execute_ShouldThrowCodeGenerationException()
        {
            // arrange
            string exceptionMessage = "Random Exception Message";
            var exception = new CodeGenerationException(exceptionMessage);
            fakes.ICodeGeneratorBuilderInteractor.Setup(x => x.Build()).Throws(exception);
            mockedSeederInteractor.Setup(x => x.CanExecute).Returns(false);

            // act
            boundary.Execute();

            // assert
            fakes.ILogger.Verify(x => x.Fatal(exception, exceptionMessage), Times.Once);
        }

        [Fact]
        public void Execute_ShouldThrowCodeException()
        {
            // arrange
            string exceptionMessage = "Random Exception Message";
            Exception exception = new(exceptionMessage);
            fakes.ICodeGeneratorBuilderInteractor.Setup(x => x.Build()).Throws(exception);
            mockedSeederInteractor.Setup(x => x.CanExecute).Returns(false);

            // act
            boundary.Execute();

            // assert
            fakes.ILogger.Verify(x => x.Fatal(exception, $"An unexpected error has occured during the expanding procecess with the following message: {exceptionMessage}."), Times.Once);
        }
    }
}
