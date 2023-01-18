using System.IO;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.CleanArchitecture.Tests.Handlers.Api
{
    public class AddAppSettingsTests
    {
        private readonly CleanArchitectureFakes fakes = new();
        private readonly AddAppSettings handler;

        public AddAppSettingsTests()
        {
            fakes.MockCleanArchitectureExpander();
            handler = new AddAppSettings(fakes.CleanArchitectureExpanderInteractor.Object, fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Constructor_ShouldValidate()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IProjectAgentInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<Parameters>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<App>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IFile>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(4));
        }

        [Fact]
        public void Order_ShouldValidate()
        {
            // arrange
            // act
            // assert
            Assert.Equal(13, handler.Order);
        }

        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(nameof(AddAppSettings), handler.Name);
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
            fakes.Parameters.Setup(x => x.GenerationMode).Returns(mode);

            // act
            // assert
            Assert.Equal(expectedResult, handler.CanExecute);
        }

        [Fact]
        public void Execute_ShouldAddConnectionStringToAppSettings_ShouldVerify()
        {
            // arrange
            string json = @"{
  ""Logging"": {
    ""LogLevel"": {
                ""Default"": ""Information"",
      ""Microsoft"": ""Warning"",
      ""Microsoft.Hosting.Lifetime"": ""Information""
    }
        },
  ""AllowedHosts"": ""*""
}";
            string jsonExpectedResult = @"{
  ""Logging"": {
    ""LogLevel"": {
      ""Default"": ""Information"",
      ""Microsoft"": ""Warning"",
      ""Microsoft.Hosting.Lifetime"": ""Information""
    }
  },
  ""AllowedHosts"": ""*"",
  ""ConnectionStrings"": {
    ""DefaultConnectionString"": ""SomeConnectionStringDefinition""
  }
}";
            string folder = "C:\\Some\\Path\\";
            string full = Path.Combine(folder, Expanders.CleanArchitecture.Resources.AppSettingsJson);
            fakes.IFile.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns(json);
            fakes.IProjectAgentInteractor.Setup(x => x.GetComponentOutputFolder(fakes.ApiComponent.Object)).Returns(folder);

            // act
            handler.Execute();

            fakes.IFile.Verify(x => x.ReadAllText(full), Times.Once);
            fakes.IFile.Verify(x => x.WriteAllText(full, jsonExpectedResult), Times.Once);
        }
    }
}