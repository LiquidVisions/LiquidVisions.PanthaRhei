﻿using System.Collections.Generic;
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
    /// <summary>
    /// Test for the <seealso cref="CleanArchitectureExpander"/>.
    /// </summary>
    public class CleanArchitectureExpanderTests
    {
        private readonly CleanArchitectureFakes fakes = new ();
        private readonly CleanArchitectureExpander expander;

        /// <summary>
        /// Initializes a new instance of the <see cref="CleanArchitectureExpanderTests"/> class.
        /// </summary>
        public CleanArchitectureExpanderTests()
        {
            fakes.MockCleanArchitectureExpander(fakes.GetValidEntities());
            expander = new CleanArchitectureExpander(fakes.IDependencyFactory.Object);
        }

        /// <summary>
        /// Dependency tests.
        /// </summary>
        [Fact]
        public void Dependencies_ShouldBeResolved()
        {
            // arrange
            // act
            // assert
            fakes.IDependencyFactory.Verify(x => x.Resolve<ICommandLine>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<GenerationOptions>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<IDirectory>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<ILogger>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<App>(), Times.Once);
            fakes.IDependencyFactory.Verify(x => x.Resolve<It.IsAnyType>(), Times.Exactly(5));
            Assert.Same(fakes.CleanArchitectureExpanderModel.Object.GetType(), expander.Model.GetType());
        }

        /// <summary>
        /// Testing the name of the <seealso cref="CleanArchitectureExpander"/>
        /// </summary>
        [Fact]
        public void Name_ShouldBeSame()
        {
            // arrange
            // act
            // assert
            Assert.Equal("CleanArchitecture", expander.Name);
        }

        /// <summary>
        /// Testing the order of the <seealso cref="CleanArchitectureExpander"/>.
        /// </summary>
        [Fact]
        public void Order_ShouldEqual()
        {
            // arrange
            CleanArchitectureExpander expanderWithoutModel = new ();

            // act
            // assert
            Assert.Equal(1, expanderWithoutModel.Order);
            Assert.Equal(2, expander.Order);
        }

        /// <summary>
        /// Testing <seealso cref="CleanArchitectureExpander"/>.GetHandlers().
        /// </summary>
        [Fact]
        public void GetHandlers_ShouldVerify()
        {
            // arrange
            // act
            expander.GetHandlers();

            // assert
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<IExpanderTask<CleanArchitectureExpander>>(), Times.Once);
        }

        /// <summary>
        /// Testing <seealso cref="CleanArchitectureExpander"/>.GetHarvesters().
        /// </summary>
        [Fact]
        public void GetHarvesters_ShouldVerify()
        {
            // arrange
            // act
            expander.GetHarvesters();

            // assert
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<IHarvester<CleanArchitectureExpander>>(), Times.Once);
        }

        /// <summary>
        /// Testing <seealso cref="CleanArchitectureExpander"/>.GetPostProcessors().
        /// </summary>
        [Fact]
        public void GetPostProcessor_ShouldVerify()
        {
            // arrange
            // act
            expander.GetPostProcessor();

            // assert
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<IPostProcessor<CleanArchitectureExpander>>(), Times.Once);
        }

        /// <summary>
        /// Testing <seealso cref="CleanArchitectureExpander"/>.GetPreProcessors().
        /// </summary>
        [Fact]
        public void GetPreProcessor_ShouldVerify()
        {
            // arrange
            // act
            expander.GetPreProcessor();

            // assert
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<IPreProcessor<CleanArchitectureExpander>>(), Times.Once);
        }

        /// <summary>
        /// Testing <seealso cref="CleanArchitectureExpander"/>.GetRejuvenators().
        /// </summary>
        [Fact]
        public void GetRejuvenators_ShouldVerify()
        {
            // arrange
            // act
            expander.GetRejuvenators();

            // assert
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<IRejuvenator<CleanArchitectureExpander>>(), Times.Once);
        }

        /// <summary>
        /// Testing <seealso cref="CleanArchitectureExpander"/>.Expand().
        /// </summary>
        [Fact]
        public void Expand_ShouldVerify()
        {
            // arrange
            Mock<IExpanderTask<CleanArchitectureExpander>> mockExpander = new ();
            mockExpander.Setup(x => x.Enabled).Returns(true);

            fakes.IDependencyFactory.Setup(x => x.ResolveAll<IExpanderTask<CleanArchitectureExpander>>()).Returns(new List<IExpanderTask<CleanArchitectureExpander>>() { mockExpander.Object });

            // act
            expander.Expand();

            // assert
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<IExpanderTask<CleanArchitectureExpander>>(), Times.Once);
            mockExpander.Verify(x => x.Execute());
        }

        /// <summary>
        /// Testing <seealso cref="CleanArchitectureExpander"/>.Harvest().
        /// </summary>
        [Fact]
        public void Harvest_ShouldVerify()
        {
            // arrange
            Mock<IHarvester<CleanArchitectureExpander>> mockExpander = new ();
            mockExpander.Setup(x => x.Enabled).Returns(true);

            fakes.IDependencyFactory.Setup(x => x.ResolveAll<IHarvester<CleanArchitectureExpander>>()).Returns(new List<IHarvester<CleanArchitectureExpander>>() { mockExpander.Object });

            // act
            expander.Harvest();

            // assert
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<IHarvester<CleanArchitectureExpander>>(), Times.Once);
            mockExpander.Verify(x => x.Execute());
        }

        /// <summary>
        /// Testing <seealso cref="CleanArchitectureExpander"/>.Rejuvenate().
        /// </summary>
        [Fact]
        public void Rejuvenate_ShouldVerify()
        {
            // arrange
            Mock<IRejuvenator<CleanArchitectureExpander>> mockExpander = new ();
            mockExpander.Setup(x => x.Enabled).Returns(true);

            fakes.IDependencyFactory.Setup(x => x.ResolveAll<IRejuvenator<CleanArchitectureExpander>>()).Returns(new List<IRejuvenator<CleanArchitectureExpander>>() { mockExpander.Object });

            // act
            expander.Rejuvenate();

            // assert
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<IRejuvenator<CleanArchitectureExpander>>(), Times.Once);
            mockExpander.Verify(x => x.Execute());
        }

        /// <summary>
        /// Testing <seealso cref="CleanArchitectureExpander"/>.PreProcess().
        /// </summary>
        [Fact]
        public void PreProcess_ShouldVerify()
        {
            // arrange
            Mock<IPreProcessor<CleanArchitectureExpander>> mockExpander = new ();
            mockExpander.Setup(x => x.Enabled).Returns(true);

            fakes.IDependencyFactory.Setup(x => x.ResolveAll<IPreProcessor<CleanArchitectureExpander>>()).Returns(new List<IPreProcessor<CleanArchitectureExpander>>() { mockExpander.Object });

            // act
            expander.PreProcess();

            // assert
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<IPreProcessor<CleanArchitectureExpander>>(), Times.Once);
            mockExpander.Verify(x => x.Execute());
        }

        /// <summary>
        /// Testing <seealso cref="CleanArchitectureExpander"/>.PostProcess().
        /// </summary>
        [Fact]
        public void PostProcess_ShouldVerify()
        {
            // arrange
            Mock<IPostProcessor<CleanArchitectureExpander>> mockExpander = new ();
            mockExpander.Setup(x => x.Enabled).Returns(true);

            fakes.IDependencyFactory.Setup(x => x.ResolveAll<IPostProcessor<CleanArchitectureExpander>>()).Returns(new List<IPostProcessor<CleanArchitectureExpander>>() { mockExpander.Object });

            // act
            expander.PostProcess();

            // assert
            fakes.IDependencyFactory.Verify(x => x.ResolveAll<IPostProcessor<CleanArchitectureExpander>>(), Times.Once);
            mockExpander.Verify(x => x.Execute());
        }

        /// <summary>
        /// Testing <seealso cref="CleanArchitectureExpander.Clean()"/>.
        /// </summary>
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
            fakes.ICommandLine.Verify(x => x.Start("dotnet user-secrets clear", apiPath), Times.Once);
            fakes.ICommandLine.Verify(x => x.Start("dotnet user-secrets clear", infraPath), Times.Once);
        }
    }
}
