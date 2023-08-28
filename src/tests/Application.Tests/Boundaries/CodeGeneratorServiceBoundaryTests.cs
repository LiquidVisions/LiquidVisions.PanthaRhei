using System;
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
    public class CodeGeneratorServiceBoundaryTests
    {
        private readonly ApplicationFakes _fakes = new();
        private readonly ExpandBoundary _boundary;
        private readonly Mock<ISeeder> _mockedSeeder = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGeneratorServiceBoundaryTests"/> class.
        /// </summary>
        public CodeGeneratorServiceBoundaryTests()
        {
            _fakes.IDependencyFactory.Setup(x => x.Get<ISeeder>()).Returns(_mockedSeeder.Object);
            _fakes.ILogManager.Setup(x => x.GetExceptionLogger()).Returns(_fakes.ILogger.Object);
            _boundary = new ExpandBoundary(_fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Dependency tests.
        /// </summary>
        [Fact]
        public void Constructor_ShouldResolve()
        {
            // arrange
            // act
            // assert
            _fakes.IDependencyFactory.Verify(x => x.Get<ICodeGeneratorBuilder>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Get<ISeeder>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Get<ILogger>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Get<ILogManager>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Get<IMigrationService>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            _fakes.IDependencyFactory.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(6));
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
        public void Execute_ExpandTests(GenerationModes modes, int times)
        {
            // arrange
            _fakes.GenerationOptions.Setup(x => x.Modes).Returns(modes);

            // act
            _boundary.Execute();

            // assert
            _fakes.ICodeGeneratorBuilder.Verify(x => x.Build(), Times.Exactly(times));
            _fakes.ICodeGenerator.Verify(x => x.Execute(), Times.Exactly(times));
        }

        /// <summary>
        /// Tests the execution conditions on <seealso cref="IMigrationService.Migrate"/>.
        /// </summary>
        /// <param name="migrate">A boolean indicating whether the migration should be executed.</param>
        /// <param name="times">The total of times the validation should be executed.</param>
        [Theory]
        [InlineData(true, 1)]
        [InlineData(false, 0)]
        public void Execute_MigrateTests(bool migrate, int times)
        {
            // arrange
            _fakes.GenerationOptions.Setup(x => x.Migrate).Returns(migrate);

            // act
            _boundary.Execute();

            // assert
            _fakes.IMigrationService.Verify(x => x.Migrate(), Times.Exactly(times));
        }

        /// <summary>
        /// Tests the execution conditions on <seealso cref="ISeeder"/>.
        /// </summary>
        /// <param name="seed">A boolean indicating whether the migration should be executed.</param>
        /// <param name="times">The total of times the validation should be executed.</param>
        [Theory]
        [InlineData(true, 1)]
        [InlineData(false, 0)]
        public void Execute_SeedTests(bool seed, int times)
        {
            // arrange
            _mockedSeeder.Setup(x => x.Enabled).Returns(seed);

            // act
            _boundary.Execute();

            // assert
            _mockedSeeder.Verify(x => x.Enabled, Times.Exactly(1));
            _mockedSeeder.Verify(x => x.Execute(), Times.Exactly(times));
        }

        /// <summary>
        /// Tests the throwing and catching of the <seealso cref="CodeGenerationException"/>.
        /// </summary>
        [Fact]
        public void Execute_ShouldThrowCodeGenerationException()
        {
            // arrange
            _fakes.GenerationOptions.Setup(x => x.Modes).Returns(GenerationModes.Default);
            string exceptionMessage = "Random Exception Message";
            var exception = new CodeGenerationException(exceptionMessage);
            _fakes.ICodeGeneratorBuilder.Setup(x => x.Build()).Throws(exception);
            _mockedSeeder.Setup(x => x.Enabled).Returns(false);

            // act
            _boundary.Execute();

            // assert
            _fakes.ILogger.Verify(x => x.Fatal(exception, exceptionMessage), Times.Once);
        }

        /// <summary>
        /// Tests the throwing and catching of the <seealso cref="Exception"/>.
        /// </summary>
        [Fact]
        public void Execute_ShouldThrowCodeException()
        {
            // arrange
            _fakes.GenerationOptions.Setup(x => x.Modes).Returns(GenerationModes.Default);
            string exceptionMessage = "Random Exception Message";
            Exception exception = new(exceptionMessage);
            _fakes.ICodeGeneratorBuilder.Setup(x => x.Build()).Throws(exception);
            _mockedSeeder.Setup(x => x.Enabled).Returns(false);

            // act
            _boundary.Execute();

            // assert
            _fakes.ILogger.Verify(x => x.Fatal(exception, $"An unexpected error has occured during the expanding procecess with the following message: {exceptionMessage}."), Times.Once);
        }
    }
}
