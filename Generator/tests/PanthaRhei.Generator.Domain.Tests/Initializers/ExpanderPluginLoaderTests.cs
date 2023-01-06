using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Initializers;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;
using LiquidVisions.PanthaRhei.Generator.Tests;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Tests.Initializers
{
    public class ExpanderPluginLoaderTests
    {
        private readonly string searchPattern = "*.Expander.*.dll";
        private readonly string expanderName = "ExpanderName";
        private readonly string pluginAssembly = @"C:\Some\Fake\Plugin.Expander.Assembly.dll";

        private readonly Fakes fakes = new();
        private readonly ExpanderPluginLoader expanderPluginLoader;
        private readonly Mock<Assembly> mockedAssembly = new();
        private readonly App app;

        public ExpanderPluginLoaderTests()
        {
            app = new() { Expanders = new List<Expander> { new Expander() { Name = expanderName } } };

            fakes.IAssemblyContext.Setup(x => x.Load(pluginAssembly)).Returns(mockedAssembly.Object);

            expanderPluginLoader = new ExpanderPluginLoader(fakes.IDependencyResolver.Object);

            fakes.IFileService.Setup(x => x.GetDirectory(fakes.Parameters.ExpandersFolder)).Returns(@"C:\Some\Fake\");
            fakes.IAssemblyContext.Setup(x => x.Load(pluginAssembly)).Returns(mockedAssembly.Object);
            fakes.IDirectoryService.Setup(x => x.GetFiles(System.IO.Path.Combine(fakes.Parameters.ExpandersFolder, expanderName), searchPattern, System.IO.SearchOption.TopDirectoryOnly)).Returns(new string[] { pluginAssembly });
        }

        [Fact]
        public void Load_RootFolderDoesNotContainPluginAssemblies_ShouldThrowException()
        {
            // arrange
            fakes.IDirectoryService.Setup(x => x.GetFiles(System.IO.Path.Combine(fakes.Parameters.ExpandersFolder, expanderName), searchPattern, System.IO.SearchOption.TopDirectoryOnly)).Returns(Array.Empty<string>());

            // act
            void Action() => expanderPluginLoader.LoadAllRegisteredPluginsAndBootstrap(app);

            // assert
            InitializationException ex = Assert.Throws<InitializationException>(Action);
            Assert.Equal($"No plugin assembly detected in '{System.IO.Path.Combine(fakes.Parameters.ExpandersFolder, expanderName)}'. The plugin assembly should match the following '{searchPattern}' pattern", ex.Message);
        }

        [Fact]
        public void Load_LoadAssemblyFilesThrowsException_ShouldRethrowWithMessage()
        {
            // arrange
            fakes.IAssemblyContext.Setup(x => x.Load(pluginAssembly)).Throws<Exception>();

            // act
            void Action() => expanderPluginLoader.LoadAllRegisteredPluginsAndBootstrap(app);

            // assert
            InitializationException ex = Assert.Throws<InitializationException>(Action);
            Assert.Equal($"Failed to load plugin '{pluginAssembly}'.", ex.Message);
        }

        [Fact]
        public void Load_ShouldVerify()
        {
            // arrange
            mockedAssembly.Setup(x => x.GetExportedTypes()).Returns(new[] { fakes.IExpanderDependencyManager.Object.GetType() });

            fakes.IObjectActivator.Setup(x => x.CreateInstance(
                fakes.IExpanderDependencyManager.Object.GetType(),
                app.Expanders.First(),
                fakes.IDependencyManager.Object,
                fakes.ILogger.Object,
                fakes.IAssemblyManager.Object))
                .Returns(fakes.IExpanderDependencyManager.Object);

            // act
            expanderPluginLoader.LoadAllRegisteredPluginsAndBootstrap(app);

            // assert
            fakes.IObjectActivator.Verify(x => x.CreateInstance(fakes.IExpanderDependencyManager.Object.GetType(), app.Expanders.First(), fakes.IDependencyManager.Object, fakes.ILogger.Object, fakes.IAssemblyManager.Object), Times.Once);
            fakes.IExpanderDependencyManager.Verify(x => x.Register(), Times.Once);
        }
    }
}
