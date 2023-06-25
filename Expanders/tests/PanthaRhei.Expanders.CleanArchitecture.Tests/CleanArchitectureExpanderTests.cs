using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.PostProcessors;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Preprocessors;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Rejuvenator;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.CleanArchitecture.Tests
{
    public class CleanArchitectureExpanderTests
    {
        private readonly CleanArchitectureFakes fakes = new();
        private readonly CleanArchitectureExpander expander;

        public CleanArchitectureExpanderTests()
        {
            fakes.MockCleanArchitectureExpander(fakes.GetValidEntities());
            expander = new CleanArchitectureExpander(fakes.IDependencyFactoryInteractor.Object);
        }

        [Fact]
        public void Dependencies_ShouldBeResolved()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ICommandLineInteractor>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<IDirectory>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<ILogger>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<App>(), Times.Once);
            fakes.IDependencyFactoryInteractor.Verify(x => x.Get<It.IsAnyType>(), Times.Exactly(5));
            Assert.Same(fakes.CleanArchitectureExpanderModel.Object.GetType(), expander.Model.GetType());
        }

        [Fact]
        public void Name_ShouldBeSame()
        {
            // arrange
            // act
            // assert
            Assert.Equal("CleanArchitecture", expander.Name);
        }

        [Fact]
        public void Order_ShouldEqual()
        {
            // arrange
            CleanArchitectureExpander expanderWithoutModel = new();

            // act
            // assert
            Assert.Equal(1, expanderWithoutModel.Order);
            Assert.Equal(2, expander.Order);
        }

        [Fact]
        public void GetHandlers_ShouldVerify()
        {
            // arrange
            // act
            expander.GetHandlers();

            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.GetAll<IExpanderTask<CleanArchitectureExpander>>(), Times.Once);
        }

        [Fact]
        public void GetHarvesters_ShouldVerify()
        {
            // arrange
            // act
            expander.GetHarvesters();

            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.GetAll<IHarvesterInteractor<CleanArchitectureExpander>>(), Times.Once);
        }

        [Fact]
        public void GetPostProcessor_ShouldVerify()
        {
            // arrange
            // act
            expander.GetPostProcessor();

            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.GetAll<IPostProcessorInteractor<CleanArchitectureExpander>>(), Times.Once);
        }

        [Fact]
        public void GetPreProcessor_ShouldVerify()
        {
            // arrange
            // act
            expander.GetPreProcessor();

            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.GetAll<IPreProcessorInteractor<CleanArchitectureExpander>>(), Times.Once);
        }

        [Fact]
        public void GetRejuvenators_ShouldVerify()
        {
            // arrange
            // act
            expander.GetRejuvenators();

            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.GetAll<IRejuvenatorInteractor<CleanArchitectureExpander>>(), Times.Once);
        }

        [Fact]
        public void Expand_ShouldVerify()
        {
            // arrange
            Mock<IExpanderTask<CleanArchitectureExpander>> mockExpander = new();
            mockExpander.Setup(x => x.Enabled).Returns(true);

            fakes.IDependencyFactoryInteractor.Setup(x => x.GetAll<IExpanderTask<CleanArchitectureExpander>>()).Returns(new List<IExpanderTask<CleanArchitectureExpander>>() { mockExpander.Object });

            // act
            expander.Expand();

            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.GetAll<IExpanderTask<CleanArchitectureExpander>>(), Times.Once);
            mockExpander.Verify(x => x.Execute());
        }

        [Fact]
        public void Harvest_ShouldVerify()
        {
            // arrange
            Mock<IHarvesterInteractor<CleanArchitectureExpander>> mockExpander = new();
            mockExpander.Setup(x => x.Enabled).Returns(true);

            fakes.IDependencyFactoryInteractor.Setup(x => x.GetAll<IHarvesterInteractor<CleanArchitectureExpander>>()).Returns(new List<IHarvesterInteractor<CleanArchitectureExpander>>() { mockExpander.Object });

            // act
            expander.Harvest();

            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.GetAll<IHarvesterInteractor<CleanArchitectureExpander>>(), Times.Once);
            mockExpander.Verify(x => x.Execute());
        }

        [Fact]
        public void Rejuvenate_ShouldVerify()
        {
            // arrange
            Mock<IRejuvenatorInteractor<CleanArchitectureExpander>> mockExpander = new();
            mockExpander.Setup(x => x.Enabled).Returns(true);

            fakes.IDependencyFactoryInteractor.Setup(x => x.GetAll<IRejuvenatorInteractor<CleanArchitectureExpander>>()).Returns(new List<IRejuvenatorInteractor<CleanArchitectureExpander>>() { mockExpander.Object });

            // act
            expander.Rejuvenate();

            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.GetAll<IRejuvenatorInteractor<CleanArchitectureExpander>>(), Times.Once);
            mockExpander.Verify(x => x.Execute());
        }

        [Fact]
        public void PreProcess_ShouldVerify()
        {
            // arrange
            Mock<IPreProcessorInteractor<CleanArchitectureExpander>> mockExpander = new();
            mockExpander.Setup(x => x.Enabled).Returns(true);

            fakes.IDependencyFactoryInteractor.Setup(x => x.GetAll<IPreProcessorInteractor<CleanArchitectureExpander>>()).Returns(new List<IPreProcessorInteractor<CleanArchitectureExpander>>() { mockExpander.Object });

            // act
            expander.PreProcess();

            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.GetAll<IPreProcessorInteractor<CleanArchitectureExpander>>(), Times.Once);
            mockExpander.Verify(x => x.Execute());
        }

        [Fact]
        public void PostProcess_ShouldVerify()
        {
            // arrange
            Mock<IPostProcessorInteractor<CleanArchitectureExpander>> mockExpander = new();
            mockExpander.Setup(x => x.Enabled).Returns(true);

            fakes.IDependencyFactoryInteractor.Setup(x => x.GetAll<IPostProcessorInteractor<CleanArchitectureExpander>>()).Returns(new List<IPostProcessorInteractor<CleanArchitectureExpander>>() { mockExpander.Object });

            // act
            expander.PostProcess();

            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.GetAll<IPostProcessorInteractor<CleanArchitectureExpander>>(), Times.Once);
            mockExpander.Verify(x => x.Execute());
        }

        [Fact]
        public void Clean_ShouldCleanUserSecrets()
        {
            // arrange
            string apiPath = "C:\\Some\\Root\\OutputFolder\\LiquidVisions.Tests\\src\\Presentation.Api";
            string infraPath = "C:\\Some\\Root\\OutputFolder\\LiquidVisions.Tests\\src\\Infrastructure.EntityFramework";
            fakes.IDirectory.Setup(x => x.Exists(apiPath)).Returns(true);
            fakes.IDirectory.Setup(x => x.Exists(infraPath)).Returns(true);

            // act
            expander.Clean();

            // assert
            fakes.IDirectory.Verify(x => x.Exists(apiPath), Times.Once);
            fakes.IDirectory.Verify(x => x.Exists(infraPath), Times.Once);
            fakes.ICommandLineInteractor.Verify(x => x.Start("dotnet user-secrets clear", apiPath), Times.Once);
            fakes.ICommandLineInteractor.Verify(x => x.Start("dotnet user-secrets clear", infraPath), Times.Once);
        }
    }
}
