using System;
using LiquidVisions.PanthaRhei.Application.Boundaries;
using LiquidVisions.PanthaRhei.Application.Usecases.Generators;
using LiquidVisions.PanthaRhei.Application.Usecases.Seeders;
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
        private readonly Mock<ISeeder> mockedSeederInteractor = new();

        public CodeGeneratorServiceBoundaryTests()
        {
            fakes.IDependencyFactory.Setup(x => x.Get<ISeeder>()).Returns(mockedSeederInteractor.Object);
            fakes.ILogManager.Setup(x => x.GetExceptionLogger()).Returns(fakes.ILogger.Object);
            boundary = new ExpandBoundary(fakes.IDependencyFactory.Object);
        }

        [Fact]
        public void Constructor_ShouldResolve()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Get<ICodeGeneratorBuilder>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<ISeeder>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<ILogger>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<ILogManager>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(5));
        }

        [Fact]
        public void Execute_ShouldBuildCodeGeneratorAndExecute()
        {
            // arrange
            mockedSeederInteractor.Setup(x => x.Enabled).Returns(false);

            // act
            boundary.Execute();

            // assert
            fakes.ICodeGeneratorBuilder.Verify(x => x.Build(), Times.Once);
            fakes.ICodeGenerator.Verify(x => x.Execute(), Times.Once);
        }

        [Fact]
        public void Execute_ShouldThrowCodeGenerationException()
        {
            // arrange
            string exceptionMessage = "Random Exception Message";
            var exception = new CodeGenerationException(exceptionMessage);
            fakes.ICodeGeneratorBuilder.Setup(x => x.Build()).Throws(exception);
            mockedSeederInteractor.Setup(x => x.Enabled).Returns(false);

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
            fakes.ICodeGeneratorBuilder.Setup(x => x.Build()).Throws(exception);
            mockedSeederInteractor.Setup(x => x.Enabled).Returns(false);

            // act
            boundary.Execute();

            // assert
            fakes.ILogger.Verify(x => x.Fatal(exception, $"An unexpected error has occured during the expanding procecess with the following message: {exceptionMessage}."), Times.Once);
        }
    }
}
