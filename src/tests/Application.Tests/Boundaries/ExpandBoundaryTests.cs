﻿using System;
using LiquidVisions.PanthaRhei.Application.Boundaries;
using LiquidVisions.PanthaRhei.Application.Usecases.Generators;
using LiquidVisions.PanthaRhei.Application.Usecases.Seeders;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Boundaries
{
    /// <summary>
    /// Tests for <seealso cref="ExpandBoundary"/>.
    /// </summary>
    public class ExpandBoundaryTests
    {
        private readonly ApplicationFakes fakes = new();
        private readonly ExpandBoundary boundary;
        private readonly Mock<ISeeder> mockedSeeder = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandBoundaryTests"/> class.
        /// </summary>
        public ExpandBoundaryTests()
        {
            fakes.IDependencyFactory.Setup(x => x.Resolve<ISeeder>()).Returns(mockedSeeder.Object);
            fakes.ILogManager.Setup(x => x.GetExceptionLogger()).Returns(fakes.ILogger.Object);
            boundary = new ExpandBoundary(fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Dependency tests.
        /// </summary>
        [Fact]
        public void ConstructorShouldResolve()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Resolve<ICodeGeneratorBuilder>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<ISeeder>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<ILogger>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<ILogManager>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IMigrationService>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(6));
        }

        /// <summary>
        /// Tests the execution conditions on <seealso cref="ICodeGenerator.Execute"/> and <seealso cref="ICodeGeneratorBuilder.Build"/>.
        /// </summary>
        /// <param name="modes"><seealso cref="GenerationModes"/>.</param>
        /// <param name="times">The total of times the validation should be executed.</param>
        [Theory]
        [InlineData(GenerationModes.Default, 1)]
        [InlineData(GenerationModes.Harvest, 1)]
        [InlineData(GenerationModes.Rejuvenate, 1)]
        [InlineData(GenerationModes.Deploy, 1)]
        [InlineData(GenerationModes.Extend, 1)]
        [InlineData(GenerationModes.Migrate, 1)]
        [InlineData(GenerationModes.Run, 1)]
        [InlineData(GenerationModes.None, 0)]
        public void ExecuteExpandTests(GenerationModes modes, int times)
        {
            // arrange
            fakes.GenerationOptions.Setup(x => x.Modes).Returns(modes);

            // act
            boundary.Execute();

            // assert
            fakes.ICodeGeneratorBuilder.Verify(x => x.Build(), Times.Exactly(times));
            fakes.ICodeGenerator.Verify(x => x.Execute(), Times.Exactly(times));
        }

        /// <summary>
        /// Tests the execution conditions on <seealso cref="IMigrationService.Migrate"/>.
        /// </summary>
        /// <param name="migrate">A boolean indicating whether the migration should be executed.</param>
        /// <param name="times">The total of times the validation should be executed.</param>
        [Theory]
        [InlineData(true, 1)]
        [InlineData(false, 0)]
        public void ExecuteMigrateTests(bool migrate, int times)
        {
            // arrange
            fakes.GenerationOptions.Setup(x => x.Migrate).Returns(migrate);

            // act
            boundary.Execute();

            // assert
            fakes.IMigrationService.Verify(x => x.Migrate(), Times.Exactly(times));
        }

        /// <summary>
        /// Tests the execution conditions on <seealso cref="ISeeder"/>.
        /// </summary>
        /// <param name="seed">A boolean indicating whether the migration should be executed.</param>
        /// <param name="times">The total of times the validation should be executed.</param>
        [Theory]
        [InlineData(true, 1)]
        [InlineData(false, 0)]
        public void ExecuteSeedTests(bool seed, int times)
        {
            // arrange
            mockedSeeder.Setup(x => x.Enabled).Returns(seed);

            // act
            boundary.Execute();

            // assert
            mockedSeeder.Verify(x => x.Enabled, Times.Exactly(1));
            mockedSeeder.Verify(x => x.Execute(), Times.Exactly(times));
        }

        /// <summary>
        /// Tests the throwing and catching of the <seealso cref="CodeGenerationException"/>.
        /// </summary>
        [Fact]
        public void ExecuteShouldThrowCodeGenerationException()
        {
            // arrange
            fakes.GenerationOptions.Setup(x => x.Modes).Returns(GenerationModes.Default);
            string exceptionMessage = "Random Exception Message";
            var exception = new CodeGenerationException(exceptionMessage);
            fakes.ICodeGeneratorBuilder.Setup(x => x.Build()).Throws(exception);
            mockedSeeder.Setup(x => x.Enabled).Returns(false);

            // act
            void action() => boundary.Execute();

            // assert
            Assert.Throws<CodeGenerationException>(action);
            fakes.ILogger.Verify(x => x.Fatal(exception, exceptionMessage), Times.Once);
        }

        /// <summary>
        /// Tests the throwing and catching of the <seealso cref="CodeGenerationException"/> while expanding.
        /// </summary>
        [Fact]
        public void ExecuteShouldThrowExpandException()
        {
            // arrange
            fakes.GenerationOptions.Setup(x => x.Modes).Returns(GenerationModes.Default);
            string exceptionMessage = "Random Exception Message";
            InvalidOperationException exception = new(exceptionMessage);
            fakes.ICodeGeneratorBuilder.Setup(x => x.Build()).Throws(exception);
            mockedSeeder.Setup(x => x.Enabled).Returns(false);

            // act
            void action() => boundary.Execute();

            // assert
            Assert.Throws<InvalidOperationException>(action);
            fakes.ILogger.Verify(x => x.Fatal(exception, $"An unexpected error has occurred during the expanding processes with the following message: {exceptionMessage}."), Times.Once);
        }

        /// <summary>
        /// Tests the throwing and catching of the <seealso cref="CodeGenerationException"/> while seeding.
        /// </summary>
        [Fact]
        public void ExecuteShouldThrowSeedException()
        {
            // arrange
            fakes.GenerationOptions.Setup(x => x.Modes).Returns(GenerationModes.Default);
            string exceptionMessage = "Random Exception Message";
            mockedSeeder.Setup(x => x.Enabled).Returns(true);

            InvalidOperationException exception = new(exceptionMessage);
            mockedSeeder.Setup(x => x.Execute()).Throws(exception);

            // act
            void action() => boundary.Execute();

            // assert
            Assert.Throws<InvalidOperationException>(action);
            fakes.ILogger.Verify(x => x.Fatal(exception, $"An unexpected error has occurred during the seeding processes with the following message: {exceptionMessage}."), Times.Once);
        }

        /// <summary>
        /// Tests the throwing and catching of the <seealso cref="CodeGenerationException"/> while seeding.
        /// </summary>
        [Fact]
        public void ExecuteShouldThrowSeedCodeGenerationException()
        {
            // arrange
            fakes.GenerationOptions.Setup(x => x.Modes).Returns(GenerationModes.Default);
            string exceptionMessage = "Random Exception Message";
            mockedSeeder.Setup(x => x.Enabled).Returns(true);
            var exception = new CodeGenerationException(exceptionMessage);
            mockedSeeder.Setup(x => x.Execute()).Throws(exception);

            // act
            void action() => boundary.Execute();

            // assert
            Assert.Throws<CodeGenerationException>(action);
            fakes.ILogger.Verify(x => x.Fatal(exception, exceptionMessage), Times.Once);
        }
    }
}
