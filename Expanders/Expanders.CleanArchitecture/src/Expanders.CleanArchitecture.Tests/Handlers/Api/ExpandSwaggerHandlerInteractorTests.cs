using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api;
using Moq;
using Xunit;
using CleanArchitectureResources = LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Resources;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests.Handlers.Api
{
    /// <summary>
    /// Tests for <seealso cref="ExpandSwaggerTask"/>
    /// </summary>
    public class ExpandSwaggerHandlerInteractorTests
    {
        private readonly CleanArchitectureFakes fakes = new ();
        private readonly ExpandSwaggerTask handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandSwaggerHandlerInteractorTests"/> class.
        /// </summary>
        public ExpandSwaggerHandlerInteractorTests()
        {
            fakes.MockCleanArchitectureExpander(new List<Entity> { fakes.ExpectedEntity });
            handler = new(fakes.CleanArchitectureExpander.Object, fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Dependency tests
        /// </summary>
        [Fact]
        public void Constructor_ShouldValidate()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<IWriter>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(2));
        }

        /// <summary>
        /// Tests for <seealso cref="ExpandSwaggerTask.Order"/>.
        /// </summary>
        [Fact]
        public void Order_ShouldValidate()
        {
            // arrange
            // act
            // assert
            Assert.Equal(12, handler.Order);
        }

        /// <summary>
        /// Tests for <seealso cref="ExpandSwaggerTask.Enabled"/>.
        /// </summary>
        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(nameof(ExpandSwaggerTask), handler.Name);
        }

        /// <summary>
        /// Test for <seealso cref="ExpandSwaggerTask.Enabled"/>.
        /// </summary>
        /// <param name="mode"><seealso cref="GenerationOptions"/>.</param>
        /// <param name="expectedResult">The expected result.</param>
        [Theory]
        [InlineData(GenerationModes.Default, true)]
        [InlineData(GenerationModes.Migrate, false)]
        [InlineData(GenerationModes.Extend, true)]
        [InlineData(GenerationModes.Deploy, false)]
        [InlineData(GenerationModes.None, false)]
        public void CanExecute_ShouldBeFalse(GenerationModes mode, bool expectedResult)
        {
            // arrange
            fakes.GenerationOptions.Setup(x => x.Modes).Returns(mode);

            // act
            // assert
            Assert.Equal(expectedResult, handler.Enabled);
        }

        /// <summary>
        /// Test for <seealso cref="ExpandSwaggerTask.Enabled"/>.
        /// </summary>
        /// <param name="clean">Tests with the Clean <seealso cref="GenerationOptions"/> mode.</param>
        /// <param name="expectedResult">The expected result.</param>
        [Theory]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public void CanExecute_ShouldOnlyBeTrueWhenCleanParameterIsSetToTrue(bool clean, bool expectedResult)
        {
            // arrange
            fakes.GenerationOptions.Setup(x => x.Modes).Returns(GenerationModes.Default);
            fakes.GenerationOptions.Setup(x => x.Clean).Returns(clean);

            // act
            // assert
            Assert.Equal(expectedResult, handler.Enabled);
        }

        /// <summary>
        /// Tests for <seealso cref="ExpandSwaggerTask.Execute()"/>.
        /// </summary>
        [Fact]
        public void Execute_ShouldAddSwaggerToConfiguration()
        {
            // arrange
            string expectedComponentOutputPath = fakes.ExpectedCompontentOutputFolder;
            string expectedMatch1 = "return services;";
            string expectedMatch2 = "app.Run();";
            string expectedPathToBootstrapperFile = Path.Combine(expectedComponentOutputPath, CleanArchitectureResources.DependencyInjectionBootstrapperFile);

            // act
            handler.Execute();

            // assert
            fakes.IWriter.Verify(x => x.Load(expectedPathToBootstrapperFile), Times.Once);
            fakes.IWriter.Verify(x => x.WriteAt(expectedMatch1, "services.AddEndpointsApiExplorer();"), Times.Once);
            fakes.IWriter.Verify(x => x.WriteAt(expectedMatch1, "services.AddSwaggerGen();"), Times.Once);
            fakes.IWriter.Verify(x => x.WriteAt(expectedMatch1, string.Empty), Times.Once);

            fakes.IWriter.Verify(x => x.WriteAt(expectedMatch2, "app.UseSwagger();"), Times.Once);
            fakes.IWriter.Verify(x => x.WriteAt(expectedMatch2, "app.UseSwaggerUI();"), Times.Once);
            fakes.IWriter.Verify(x => x.WriteAt(expectedMatch1, string.Empty), Times.Once);

            fakes.IWriter.Verify(x => x.Save(expectedPathToBootstrapperFile), Times.Once);
        }
    }
}
