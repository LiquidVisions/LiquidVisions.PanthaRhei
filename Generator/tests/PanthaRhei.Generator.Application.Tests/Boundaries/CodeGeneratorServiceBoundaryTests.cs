using System;
using LiquidVisions.PanthaRhei.Generator.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;
using LiquidVisions.PanthaRhei.Generator.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.Application.Tests.Boundaries
{
    public class CodeGeneratorServiceBoundaryTests
    {
        private readonly Fakes fakes = new();
        private readonly CodeGeneratorServiceBoundary boundary;

        public CodeGeneratorServiceBoundaryTests()
        {
            fakes.ILogManager.Setup(x => x.GetExceptionLogger()).Returns(fakes.ILogger.Object);
            boundary = new CodeGeneratorServiceBoundary(fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Constructor_ShouldResolve()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ICodeGeneratorBuilderInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ILogger>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ILogManager>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(3));
        }

        [Fact]
        public void Execute_ShouldBuildCodeGeneratorAndExecute()
        {
            // arrange
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

            // act
            boundary.Execute();

            // assert
            fakes.ILogger.Verify(x => x.Fatal(exception, $"An unexpected error has occured with the following message: {exceptionMessage}."), Times.Once);
        }
    }
}
