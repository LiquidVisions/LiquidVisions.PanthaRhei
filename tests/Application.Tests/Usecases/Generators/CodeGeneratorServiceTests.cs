using System;
using LiquidVisions.PanthaRhei.Application.Boundaries;
using LiquidVisions.PanthaRhei.Application.Usecases.Generators;
using LiquidVisions.PanthaRhei.Application.Usecases.Seeders;
using LiquidVisions.PanthaRhei.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Usecases.Generators
{
    public class CodeGeneratorServiceTests
    {
        private readonly Fakes fakes = new();
        private readonly ExpandBoundary service;
        private readonly Mock<ISeeder> mockedSeederInteractor = new();

        public CodeGeneratorServiceTests()
        {
            fakes.IDependencyFactory.Setup(x => x.Get<ISeeder>()).Returns(mockedSeederInteractor.Object);
            service = new ExpandBoundary(fakes.IDependencyFactory.Object);
        }

        [Fact]
        public void Execute_ShouldVerify()
        {
            // arrange
            mockedSeederInteractor.Setup(x => x.Enabled).Returns(false);

            // act
            service.Execute();

            // assert
            fakes.ICodeGeneratorBuilder.Verify(x => x.Build(), Times.Once);
            fakes.ICodeGenerator.Verify(x => x.Execute(), Times.Once);
        }

        [Fact]
        public void Execute_CodeGenerationException_ShouldCatchAndLogFatal()
        {
            // arrange
            string exceptionMessage = "Dit is an exception message";
            CodeGenerationException exception = new(exceptionMessage);
            fakes.ICodeGeneratorBuilder.Setup(x => x.Build()).Throws(exception);
            mockedSeederInteractor.Setup(x => x.Enabled).Returns(false);

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
            fakes.ICodeGeneratorBuilder.Setup(x => x.Build()).Throws(exception);
            mockedSeederInteractor.Setup(x => x.Enabled).Returns(false);

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
            mockedSeederInteractor.Setup(x => x.Enabled).Returns(true);

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
            mockedSeederInteractor.Setup(x => x.Enabled).Returns(true);

            // act
            service.Execute();

            // assert
            fakes.ILogger.Verify(x => x.Fatal(exception, It.Is<string>(x => x == $"An unexpected error has occured during the seeding processes with the following message: {exception.Message}.")), Times.Once);
        }
    }
}
