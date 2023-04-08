using System;
using LiquidVisions.PanthaRhei.Generator.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders;
using LiquidVisions.PanthaRhei.Generator.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.Application.Tests.Interactors.Generators
{
    public class CodeGeneratorServiceTests
    {
        private readonly Fakes fakes = new();
        private readonly ExpandBoundary service;
        private readonly Mock<ISeederInteractor> mockedSeederInteractor = new();

        public CodeGeneratorServiceTests()
        {
            fakes.IDependencyFactoryInteractor.Setup(x => x.Get<ISeederInteractor>()).Returns(mockedSeederInteractor.Object);
            service = new ExpandBoundary(fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Execute_ShouldVerify()
        {
            // arrange
            mockedSeederInteractor.Setup(x => x.CanExecute).Returns(false);

            // act
            service.Execute();

            // assert
            fakes.ICodeGeneratorBuilderInteractor.Verify(x => x.Build(), Times.Once);
            fakes.ICodeGeneratorInteractor.Verify(x => x.Execute(), Times.Once);
        }

        [Fact]
        public void Execute_CodeGenerationException_ShouldCatchAndLogFatal()
        {
            // arrange
            string exceptionMessage = "Dit is an exception message";
            CodeGenerationException exception = new(exceptionMessage);
            fakes.ICodeGeneratorBuilderInteractor.Setup(x => x.Build()).Throws(exception);
            mockedSeederInteractor.Setup(x => x.CanExecute).Returns(false);

            // act
            service.Execute();

            // assert
            fakes.ILogger.Verify(x => x.Fatal(exception, exceptionMessage), Times.Once);
        }

        [Fact]
        public void Execute_UnexpectedException_ShouldCatchAndLogFatal()
        {
            // arrange
            string exceptionMessage = "Dit is an exception message";
            InvalidOperationException exception = new(exceptionMessage);
            fakes.ICodeGeneratorBuilderInteractor.Setup(x => x.Build()).Throws(exception);
            mockedSeederInteractor.Setup(x => x.CanExecute).Returns(false);

            // act
            service.Execute();

            // assert
            fakes.ILogger.Verify(x => x.Fatal(exception, It.Is<string>(x => x == $"An unexpected error has occured during the expanding procecess with the following message: {exception.Message}.")), Times.Once);
        }

        [Fact]
        public void Execute_SeedException_ShouldCatchAndLogFatal()
        {
            // arrange
            string exceptionMessage = "Dit is an exception message";
            CodeGenerationException exception = new(exceptionMessage);
            mockedSeederInteractor.Setup(x => x.Execute()).Throws(exception);
            mockedSeederInteractor.Setup(x => x.CanExecute).Returns(true);

            // act
            service.Execute();

            // assert
            fakes.ILogger.Verify(x => x.Fatal(exception, exceptionMessage), Times.Once);
        }

        [Fact]
        public void Execute_UnexpectedSeedException_ShouldCatchAndLogFatal()
        {
            // arrange
            string exceptionMessage = "Dit is an exception message";
            InvalidOperationException exception = new(exceptionMessage);
            mockedSeederInteractor.Setup(x => x.Execute()).Throws(exception);
            mockedSeederInteractor.Setup(x => x.CanExecute).Returns(true);

            // act
            service.Execute();

            // assert
            fakes.ILogger.Verify(x => x.Fatal(exception, It.Is<string>(x => x == $"An unexpected error has occured during the seeding processes with the following message: {exception.Message}.")), Times.Once);
        }
    }
}
