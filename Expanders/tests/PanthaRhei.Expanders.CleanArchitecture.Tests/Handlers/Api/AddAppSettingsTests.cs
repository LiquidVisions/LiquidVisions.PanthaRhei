using System.IO;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.CleanArchitecture.Tests.Handlers.Api
{
    public class AddAppSettingsTests : AbstractCleanArchitectureTests
    {
        private readonly AddAppSettings handler;

        public AddAppSettingsTests()
        {
            Fakes.MockAppWithMockedExpanders();
            handler = new AddAppSettings(Fakes.CleanArchitectureExpanderInteractor.Object, Fakes.IDependencyFactoryInteractor.Object);
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
            string full = Path.Combine(folder, Resources.AppSettingsJson);
            Fakes.IFile.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns(json);
            Fakes.IProjectAgentInteractor.Setup(x => x.GetComponentOutputFolder(Fakes.ApiComponent.Object)).Returns(folder);

            // act
            handler.Execute();

            Fakes.IFile.Verify(x => x.ReadAllText(full), Times.Once);
            Fakes.IFile.Verify(x => x.WriteAllText(full, jsonExpectedResult), Times.Once);
        }
    }
}