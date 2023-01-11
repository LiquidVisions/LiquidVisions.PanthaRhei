﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Initializers;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Initializers;
using LiquidVisions.PanthaRhei.Generator.Tests;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.Application.Tests.Initializers
{
    public class ExpanderPluginLoaderInteractorTests
    {
        private readonly string searchPattern = "*.Expanders.*.dll";
        private readonly string expanderName = "ExpanderName";
        private readonly string pluginAssembly = @"C:\Some\Fake\Plugin.Expanders.Assembly.dll";

        private readonly Fakes fakes = new();
        private readonly ExpanderPluginLoaderInteractor interactor;
        private readonly Mock<Assembly> mockedAssembly = new();
        private readonly App app;

        public ExpanderPluginLoaderInteractorTests()
        {
            app = new() { Expanders = new List<Expander> { new Expander() { Name = expanderName } } };

            fakes.IAssemblyContextInteractor.Setup(x => x.Load(pluginAssembly)).Returns(mockedAssembly.Object);

            interactor = new ExpanderPluginLoaderInteractor(fakes.IDependencyFactoryInteractor.Object);

            fakes.IFile.Setup(x => x.GetDirectory(fakes.Parameters.ExpandersFolder)).Returns(@"C:\Some\Fake\");
            fakes.IAssemblyContextInteractor.Setup(x => x.Load(pluginAssembly)).Returns(mockedAssembly.Object);
            fakes.IDirectory.Setup(x => x.GetFiles(System.IO.Path.Combine(fakes.Parameters.ExpandersFolder, expanderName), searchPattern, System.IO.SearchOption.TopDirectoryOnly)).Returns(new string[] { pluginAssembly });
        }

        [Fact]
        public void Load_RootFolderDoesNotContainPluginAssemblies_ShouldThrowException()
        {
            // arrange
            fakes.IDirectory.Setup(x => x.GetFiles(System.IO.Path.Combine(fakes.Parameters.ExpandersFolder, expanderName), searchPattern, System.IO.SearchOption.TopDirectoryOnly)).Returns(Array.Empty<string>());

            // act
            void Action() => interactor.LoadAllRegisteredPluginsAndBootstrap(app);

            // assert
            InitializationException ex = Assert.Throws<InitializationException>(Action);
            Assert.Equal($"No plugin assembly detected in '{System.IO.Path.Combine(fakes.Parameters.ExpandersFolder, expanderName)}'. The plugin assembly should match the following '{searchPattern}' pattern", ex.Message);
        }

        [Fact]
        public void Load_LoadAssemblyFilesThrowsException_ShouldRethrowWithMessage()
        {
            // arrange
            fakes.IAssemblyContextInteractor.Setup(x => x.Load(pluginAssembly)).Throws<Exception>();

            // act
            void Action() => interactor.LoadAllRegisteredPluginsAndBootstrap(app);

            // assert
            InitializationException ex = Assert.Throws<InitializationException>(Action);
            Assert.Equal($"Failed to load plugin '{pluginAssembly}'.", ex.Message);
        }

        [Fact]
        public void Load_ShouldVerify()
        {
            // arrange
            mockedAssembly.Setup(x => x.GetExportedTypes()).Returns(new[] { fakes.IExpanderDependencyManagerInteractor.Object.GetType() });

            fakes.IObjectActivatorInteractor.Setup(x => x.CreateInstance(
                fakes.IExpanderDependencyManagerInteractor.Object.GetType(),
                app.Expanders.First(),
                fakes.IDependencyManagerInteractor.Object))
                .Returns(fakes.IExpanderDependencyManagerInteractor.Object);

            // act
            interactor.LoadAllRegisteredPluginsAndBootstrap(app);

            // assert
            fakes.IObjectActivatorInteractor.Verify(x => x.CreateInstance(fakes.IExpanderDependencyManagerInteractor.Object.GetType(), app.Expanders.First(), fakes.IDependencyManagerInteractor.Object), Times.Once);
            fakes.IExpanderDependencyManagerInteractor.Verify(x => x.Register(), Times.Once);
        }
    }
}