using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LiquidVisions.PanthaRhei.Application.Usecases.Initializers;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Initializers;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Application.Tests.Initializers
{
    /// <summary>
    /// Tests for <seealso cref="ExpanderPluginLoader"/>.
    /// </summary>
    public class ExpanderPluginLoaderTests
    {
        private readonly string searchPattern = "*.Expanders.*.dll";
        private readonly string expanderName = "ExpanderName";
        private readonly string pluginAssembly = @"C:\Some\Fake\Plugin.Expanders.Assembly.dll";

        private readonly ApplicationFakes fakes = new();
        private readonly ExpanderPluginLoader interactor;
        private readonly Mock<Assembly> mockedEntryAssembly = new();
        private readonly Mock<Assembly> mockedAssembly = new();
        private readonly App app;
        private readonly Version version = new(1, 1, 1, 1);

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpanderPluginLoaderTests"/> class.
        /// </summary>
        public ExpanderPluginLoaderTests()
        {
            app = new();
            app.Expanders.Add(new Expander() { Name = expanderName });

            fakes.IAssemblyContext.Setup(x => x.Load(pluginAssembly)).Returns(mockedAssembly.Object);
            mockedEntryAssembly.Setup(x => x.GetName()).Returns(new AssemblyName("EntryAssembly") { Version = version });
            fakes.IAssemblyProvider.Setup(x => x.EntryAssembly).Returns(mockedEntryAssembly.Object);

            interactor = new ExpanderPluginLoader(fakes.IDependencyFactory.Object);

            fakes.IFile.Setup(x => x.GetDirectory(fakes.GenerationOptions.Object.ExpandersFolder)).Returns(@"C:\Some\Fake\");
            fakes.IAssemblyContext.Setup(x => x.Load(pluginAssembly)).Returns(mockedAssembly.Object);
            fakes.IDirectory.Setup(x => x.GetFiles(Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, expanderName), searchPattern, SearchOption.TopDirectoryOnly)).Returns([pluginAssembly]);
        }

        /// <summary>
        /// Tests for <seealso cref="ExpanderPluginLoader.LoadAllRegisteredPluginsAndBootstrap(App)"/> while throwing <seealso cref="InitializationException"/>.
        /// </summary>
        [Fact]
        public void LoadRootFolderDoesNotContainPluginAssembliesShouldThrowException()
        {
            // arrange
            fakes.IDirectory.Setup(x => x.GetFiles(Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, expanderName), searchPattern, SearchOption.TopDirectoryOnly)).Returns([]);

            // act
            void Action() => interactor.LoadAllRegisteredPluginsAndBootstrap(app);

            // assert
            InitializationException ex = Assert.Throws<InitializationException>(Action);
            Assert.Equal($"No plugin assembly detected in '{Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, expanderName)}'. The plugin assembly should match the following '{searchPattern}' pattern", ex.Message);
        }

        /// <summary>
        /// Tests for <seealso cref="ExpanderPluginLoader.LoadAllRegisteredPluginsAndBootstrap(App)"/> while throwing <seealso cref="InitializationException"/>.
        /// </summary>
        [Fact]
        public void LoadLoadAssemblyFilesThrowsExceptionShouldRethrowWithMessage()
        {
            // arrange
            fakes.IAssemblyProvider.Setup(x => x.EntryAssembly).Returns(mockedEntryAssembly.Object);
            


            fakes.IAssemblyContext.Setup(x => x.Load(pluginAssembly)).Throws<Exception>();

            // act
            void Action() => interactor.LoadAllRegisteredPluginsAndBootstrap(app);

            // assert
            InitializationException ex = Assert.Throws<InitializationException>(Action);
            Assert.StartsWith($"Failed to load plugin '{pluginAssembly}'.", ex.Message, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests from <seealso cref="ExpanderPluginLoader.LoadAllRegisteredPluginsAndBootstrap(App)"/> in happy flow.
        /// </summary>
        [Fact]
        public void LoadShouldVerify()
        {
            // arrange
            mockedAssembly.Setup(x => x.GetExportedTypes()).Returns([fakes.IExpanderDependencyManager.Object.GetType()]);
            mockedAssembly.Setup(x => x.GetReferencedAssemblies()).Returns([new AssemblyName(Resources.PackageAssemblyName) { Version = version }]);

            fakes.IObjectActivator.Setup(x => x.CreateInstance(
                fakes.IExpanderDependencyManager.Object.GetType(),
                app.Expanders.First(),
                fakes.IDependencyManager.Object))
                .Returns(fakes.IExpanderDependencyManager.Object);

            // act
            interactor.LoadAllRegisteredPluginsAndBootstrap(app);

            // assert
            fakes.IObjectActivator.Verify(x => x.CreateInstance(fakes.IExpanderDependencyManager.Object.GetType(), app.Expanders.First(), fakes.IDependencyManager.Object), Times.Once);
            fakes.IExpanderDependencyManager.Verify(x => x.Register(), Times.Once);
        }

        /// <summary>
        /// Tests from <seealso cref="ExpanderPluginLoader.LoadAllRegisteredPluginsAndBootstrap(App)"/> where the assembly version is incompatible.
        /// </summary>
        [Fact]
        public void LoadShouldThrowInitializationExceptionBecauseAssemblyVersionsAreIncompatible()
        {
            // arrange
            mockedAssembly.Setup(x => x.GetExportedTypes()).Returns([fakes.IExpanderDependencyManager.Object.GetType()]);
            mockedAssembly.Setup(x => x.GetReferencedAssemblies()).Returns([new AssemblyName(Resources.PackageAssemblyName) { Version = new Version("1.0.0.0") }]);

            fakes.IObjectActivator.Setup(x => x.CreateInstance(
                fakes.IExpanderDependencyManager.Object.GetType(),
                app.Expanders.First(),
                fakes.IDependencyManager.Object))
                .Returns(fakes.IExpanderDependencyManager.Object);

            // act & assert
            InitializationException exception = Assert.Throws<InitializationException>(() => interactor.LoadAllRegisteredPluginsAndBootstrap(app));
            Assert.StartsWith($"Failed to load plugin '{pluginAssembly}'. LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Initializers.InitializationException: Incompatible versions used. Entry assembly version: {version}, Plugin assembly version: 1.0.0.0", exception.Message, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests for <seealso cref="ExpanderPluginLoader.ShallowLoadAllExpanders(string)"/>.
        /// </summary>
        [Fact]
        public void ShallowLoadShouldVerify()
        {
            // arrange
            string path = "C:\\Some\\Fake\\";

            Mock<IExpander> mockedIExpander = new();

            mockedAssembly
                .Setup(x => x.GetExportedTypes())
                .Returns([mockedIExpander.Object.GetType()]);

            fakes.IDirectory
                .Setup(x => x.GetFiles(path, searchPattern, SearchOption.AllDirectories))
                .Returns([pluginAssembly]);

            fakes.IAssemblyContext
                .Setup(x => x.Load(pluginAssembly))
                .Returns(mockedAssembly.Object);

            fakes.IObjectActivator.Setup(x => x.CreateInstance(
                fakes.IExpanderDependencyManager.Object.GetType()))
                .Returns(fakes.IExpanderDependencyManager.Object);

            // act
            IEnumerable<IExpander> result = interactor.ShallowLoadAllExpanders(path);

            // assert
            fakes.IObjectActivator.Verify(x => x.CreateInstance(It.IsAny<Type>()), Times.Once);
            Assert.Single(result);
        }
    }
}
