﻿using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.PostProcessors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Preprocessors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Rejuvenator;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.CleanArchitecture.Tests
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
            fakes.IDependencyFactoryInteractor.Verify(x => x.GetAll<IExpanderHandlerInteractor<CleanArchitectureExpander>>(), Times.Once);
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
            Mock<IExpanderHandlerInteractor<CleanArchitectureExpander>> mockExpander = new();
            mockExpander.Setup(x => x.CanExecute).Returns(true);

            fakes.IDependencyFactoryInteractor.Setup(x => x.GetAll<IExpanderHandlerInteractor<CleanArchitectureExpander>>()).Returns(new List<IExpanderHandlerInteractor<CleanArchitectureExpander>>() { mockExpander.Object });

            // act
            expander.Expand();

            // assert
            fakes.IDependencyFactoryInteractor.Verify(x => x.GetAll<IExpanderHandlerInteractor<CleanArchitectureExpander>>(), Times.Once);
            mockExpander.Verify(x => x.Execute());
        }

        [Fact]
        public void Harvest_ShouldVerify()
        {
            // arrange
            Mock<IHarvesterInteractor<CleanArchitectureExpander>> mockExpander = new();
            mockExpander.Setup(x => x.CanExecute).Returns(true);

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
            mockExpander.Setup(x => x.CanExecute).Returns(true);

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
            mockExpander.Setup(x => x.CanExecute).Returns(true);

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
            mockExpander.Setup(x => x.CanExecute).Returns(true);

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