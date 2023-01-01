using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generator.Domain.UseCases;
using LiquidVisions.PanthaRhei.Generator.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.Application.Tests
{
    public class CodeGeneratorServiceTests
    {
        private readonly Fakes fakes = new();
        private readonly CodeGeneratorService service;

        public CodeGeneratorServiceTests()
        {
            service = new CodeGeneratorService(fakes.IDependencyResolver.Object);
        }

        [Fact]
        public void Execute_ShouldVerify()
        {
            // arrange
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
            CodeGenerationException exception = new (exceptionMessage);
            fakes.ICodeGeneratorBuilder.Setup(x => x.Build()).Throws(exception);

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

            // act
            service.Execute();

            // assert
            fakes.ILogger.Verify(x => x.Fatal(exception, It.Is<string>(x => x == $"An unexpected error has occured with the following message: {exception.Message}.")), Times.Once);
        }
    }
}
