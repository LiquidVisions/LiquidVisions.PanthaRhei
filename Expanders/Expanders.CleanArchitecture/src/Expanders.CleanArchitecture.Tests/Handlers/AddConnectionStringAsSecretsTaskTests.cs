using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests.Handlers
{
    /// <summary>
    /// Test for <seealso cref="AddConnectionStringAsSecretsTask"/>.
    /// </summary>
    public class AddConnectionStringAsSecretsTaskTests
    {
        private readonly CleanArchitectureFakes fakes = new ();
        private readonly AddConnectionStringAsSecretsTask task;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConnectionStringAsSecretsTaskTests"/> class.
        /// </summary>
        public AddConnectionStringAsSecretsTaskTests()
        {
            App app = fakes.SetupApp();
            fakes.CleanArchitectureExpander.Setup(x => x.App).Returns(app);
            task = new AddConnectionStringAsSecretsTask(fakes.CleanArchitectureExpander.Object, fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Tests dependencies of <seealso cref="AddConnectionStringAsSecretsTask"/>.
        /// </summary>
        [Fact]
        public void Dependencies_ShouldBeResolved()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Get<ICommandLine>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<App>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(3));
        }

        /// <summary>
        /// Tests <seealso cref="AddConnectionStringAsSecretsTask.Order"/>.
        /// </summary>
        [Fact]
        public void Order_ShouldBeEqual()
        {
            // arrange
            // act
            // assert
            Assert.Equal(16, task.Order);
        }

        /// <summary>
        /// Tests <seealso cref="AddConnectionStringAsSecretsTask.Name"/>.
        /// </summary>
        [Fact]
        public void Name_ShouldBeEqual()
        {
            // arrange
            // act
            string name = task.Name;

            // assert
            Assert.Equal(nameof(AddConnectionStringAsSecretsTaskTests), name);
        }

        /// <summary>
        /// Tests <seealso cref="AddConnectionStringAsSecretsTask.Enabled"/>.
        /// </summary>
        /// <param name="mode">The <seealso cref="GenerationModes">GenerateMode</seealso> that is tested.</param>
        /// <param name="expectedResult">The expected result.</param>.
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
            Assert.Equal(expectedResult, task.Enabled);
        }

        /// <summary>
        /// Tests for <seealso cref="AddConnectionStringAsSecretsTask.Execute()"/>.
        /// </summary>
        [Fact]
        public void Execute_ShouldExecuteCommand()
        {
            // arrange
            string path1 = "C:\\Path1";
            string path2 = "C:\\Path2";
            fakes.CleanArchitectureExpander.Setup(x => x.GetComponentPaths(Expanders.CleanArchitecture.Resources.Api, Expanders.CleanArchitecture.Resources.EntityFramework)).Returns(new List<string>() { path1, path2 });

            // act
            task.Execute();

            // assert
            fakes.ICommandLine.Verify(x => x.Start("dotnet user-secrets init", path1), Times.Once);
            fakes.ICommandLine.Verify(x => x.Start("dotnet user-secrets init", path2), Times.Once);
            fakes.ICommandLine.Verify(x => x.Start($"dotnet user-secrets set \"ConnectionStrings:DefaultConnectionString\" \"SomeConnectionStringDefinition\"", path1), Times.Once);
            fakes.ICommandLine.Verify(x => x.Start($"dotnet user-secrets set \"ConnectionStrings:DefaultConnectionString\" \"SomeConnectionStringDefinition\"", path2), Times.Once);
        }
    }
}
