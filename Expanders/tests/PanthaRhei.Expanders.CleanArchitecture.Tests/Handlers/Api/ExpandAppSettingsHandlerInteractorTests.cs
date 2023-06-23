﻿using System.IO;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.CleanArchitecture.Tests.Handlers.Api
{
    public class ExpandAppSettingsHandlerInteractorTests
    {
        private readonly CleanArchitectureFakes fakes = new();
        private readonly ExpandAppSettingsHandlerInteractor handler;
        private readonly string json = @"{
  ""Logging"": {
    ""LogLevel"": {
                ""Default"": ""Information"",
      ""Microsoft"": ""Warning"",
      ""Microsoft.Hosting.Lifetime"": ""Information""
    }
        },
  ""AllowedHosts"": ""*""
}";

        private readonly string jsonExpectedResult = @"{
  ""Logging"": {
    ""LogLevel"": {
      ""Default"": ""Information"",
      ""Microsoft"": ""Warning"",
      ""Microsoft.Hosting.Lifetime"": ""Information""
    }
  },
  ""AllowedHosts"": ""*"",
  ""ConnectionStrings"": {
    ""DefaultConnectionString"": ""CONNECTIONSTRING_IS_USER-SECRET""
  }
}";

        public ExpandAppSettingsHandlerInteractorTests()
        {
            fakes.IFile.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns(json);
            fakes.MockCleanArchitectureExpander();
            handler = new ExpandAppSettingsHandlerInteractor(fakes.CleanArchitectureExpander.Object, fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Constructor_ShouldValidate()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<App>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IFile>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IWriterInteractor>(), Times.Once);
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
            Assert.Equal(nameof(ExpandAppSettingsHandlerInteractor), handler.Name);
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
            Assert.Equal(expectedResult, handler.CanExecute);
        }

        [Fact]
        public void Execute_ShouldAddConnectionStringToAppSettings_ShouldVerify()
        {
            // arrange
            string appSettingsFile = Path.Combine(fakes.ExpectedCompontentOutputFolder, Expanders.CleanArchitecture.Resources.AppSettingsJson);
            string bootStrapFile = Path.Combine(fakes.ExpectedCompontentOutputFolder, Expanders.CleanArchitecture.Resources.DependencyInjectionBootstrapperFile);

            // act
            handler.Execute();

            fakes.IFile.Verify(x => x.ReadAllText(appSettingsFile), Times.Once);
            fakes.IFile.Verify(x => x.WriteAllText(appSettingsFile, jsonExpectedResult), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.Load(bootStrapFile), Times.Once);
            fakes.IWriterInteractor.Verify(x => x.Replace("CONNECTION_STRING_PLACEHOLDER", "DefaultConnectionString"));
            fakes.IWriterInteractor.Verify(x => x.Save(bootStrapFile), Times.Once);
        }
    }
}