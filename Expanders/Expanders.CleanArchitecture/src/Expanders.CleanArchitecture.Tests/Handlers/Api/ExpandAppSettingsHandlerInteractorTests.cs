using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests.Handlers.Api
{
    /// <summary>
    /// Test for <seealso cref="ExpandAppSettingsTask"/>.
    /// </summary>
    public class ExpandAppSettingsHandlerInteractorTests
    {
        private readonly CleanArchitectureFakes fakes = new ();
        private readonly ExpandAppSettingsTask handler;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandAppSettingsHandlerInteractorTests"/> class.
        /// </summary>
        public ExpandAppSettingsHandlerInteractorTests()
        {
            fakes.IFile.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns(json);
            fakes.MockCleanArchitectureExpander();
            handler = new ExpandAppSettingsTask(fakes.CleanArchitectureExpander.Object, fakes.IDependencyFactory.Object);
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
            fakes.IDependencyFactory.Verify(x => x.Get<App>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<IFile>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<IWriter>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(4));
        }

        /// <summary>
        /// Test for <seealso cref="ExpandAppSettingsTask.Order"/>.
        /// </summary>
        [Fact]
        public void Order_ShouldValidate()
        {
            // arrange
            // act
            // assert
            Assert.Equal(13, handler.Order);
        }

        /// <summary>
        /// Test for <seealso cref="ExpandAppSettingsTask.Name"/>.
        /// </summary>
        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(nameof(ExpandAppSettingsTask), handler.Name);
        }

        /// <summary>
        /// Tests for <seealso cref="ExpandAppSettingsTask.Enabled"/>.
        /// </summary>
        /// <param name="mode"><seealso cref="GenerationModes"/> to tests</param>
        /// <param name="expectedResult">The expected result</param>
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
        /// Test for <seealso cref="ExpandAppSettingsTask.Execute()"/>.
        /// </summary>
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
            fakes.IWriter.Verify(x => x.Load(bootStrapFile), Times.Once);
            fakes.IWriter.Verify(x => x.Replace("CONNECTION_STRING_PLACEHOLDER", "DefaultConnectionString"));
            fakes.IWriter.Verify(x => x.Save(bootStrapFile), Times.Once);
        }
    }
}