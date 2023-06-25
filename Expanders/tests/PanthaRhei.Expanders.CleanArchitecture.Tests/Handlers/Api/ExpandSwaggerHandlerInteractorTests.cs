using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using Moq;
using Xunit;
using CleanArchitectureResources = LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Resources;
using LiquidVisions.PanthaRhei.Domain.Usecases;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests.Handlers.Api
{
    public class ExpandSwaggerHandlerInteractorTests
    {
        private readonly CleanArchitectureFakes fakes = new();
        private readonly ExpandSwaggerHandlerInteractor handler;

        public ExpandSwaggerHandlerInteractorTests()
        {
            fakes.MockCleanArchitectureExpander(new List<Entity> { fakes.ExpectedEntity });
            handler = new(fakes.CleanArchitectureExpander.Object, fakes.IDependencyFactory.Object);
        }

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

        [Fact]
        public void Order_ShouldValidate()
        {
            // arrange
            // act
            // assert
            Assert.Equal(12, handler.Order);
        }

        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(nameof(ExpandSwaggerHandlerInteractor), handler.Name);
        }

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
