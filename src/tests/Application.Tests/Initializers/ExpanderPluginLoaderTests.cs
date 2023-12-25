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
        private readonly string _searchPattern = "*.Expanders.*.dll";
        private readonly string _expanderName = "ExpanderName";
        private readonly string _pluginAssembly = @"C:\Some\Fake\Plugin.Expanders.Assembly.dll";

        private readonly ApplicationFakes _fakes = new();
        private readonly ExpanderPluginLoader _interactor;
        private readonly Mock<Assembly> _mockedEntryAssembly = new();
        private readonly Mock<Assembly> _mockedAssembly = new();
        private readonly App _app;
        private readonly Version _version = new(1, 1, 1, 1);

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpanderPluginLoaderTests"/> class.
        /// </summary>
        public ExpanderPluginLoaderTests()
        {
            _app = new();
            _app.Expanders.Add(new Expander() { Name = _expanderName });

            _fakes.IAssemblyContext.Setup(x => x.Load(_pluginAssembly)).Returns(_mockedAssembly.Object);
            _mockedEntryAssembly.Setup(x => x.GetName()).Returns(new AssemblyName("EntryAssembly") { Version = _version });
            _fakes.IAssemblyProvider.Setup(x => x.EntryAssembly).Returns(_mockedEntryAssembly.Object);

            _interactor = new ExpanderPluginLoader(_fakes.IDependencyFactory.Object);

            _fakes.IFile.Setup(x => x.GetDirectory(_fakes.GenerationOptions.Object.ExpandersFolder)).Returns(@"C:\Some\Fake\");
            _fakes.IAssemblyContext.Setup(x => x.Load(_pluginAssembly)).Returns(_mockedAssembly.Object);
            _fakes.IDirectory.Setup(x => x.GetFiles(Path.Combine(_fakes.GenerationOptions.Object.ExpandersFolder, _expanderName), _searchPattern, SearchOption.TopDirectoryOnly)).Returns(new string[] { _pluginAssembly });
        }

        /// <summary>
        /// Tests for <seealso cref="ExpanderPluginLoader.LoadAllRegisteredPluginsAndBootstrap(App)"/> while throwing <seealso cref="InitializationException"/>.
        /// </summary>
        [Fact]
        public void LoadRootFolderDoesNotContainPluginAssembliesShouldThrowException()
        {
            // arrange
            _fakes.IDirectory.Setup(x => x.GetFiles(Path.Combine(_fakes.GenerationOptions.Object.ExpandersFolder, _expanderName), _searchPattern, SearchOption.TopDirectoryOnly)).Returns(Array.Empty<string>());

            // act
            void Action() => _interactor.LoadAllRegisteredPluginsAndBootstrap(_app);

            // assert
            InitializationException ex = Assert.Throws<InitializationException>(Action);
            Assert.Equal($"No plugin assembly detected in '{Path.Combine(_fakes.GenerationOptions.Object.ExpandersFolder, _expanderName)}'. The plugin assembly should match the following '{_searchPattern}' pattern", ex.Message);
        }

        /// <summary>
        /// Tests for <seealso cref="ExpanderPluginLoader.LoadAllRegisteredPluginsAndBootstrap(App)"/> while throwing <seealso cref="InitializationException"/>.
        /// </summary>
        [Fact]
        public void LoadLoadAssemblyFilesThrowsExceptionShouldRethrowWithMessage()
        {
            // arrange
            _fakes.IAssemblyProvider.Setup(x => x.EntryAssembly).Returns(_mockedEntryAssembly.Object);
            


            _fakes.IAssemblyContext.Setup(x => x.Load(_pluginAssembly)).Throws<Exception>();

            // act
            void Action() => _interactor.LoadAllRegisteredPluginsAndBootstrap(_app);

            // assert
            InitializationException ex = Assert.Throws<InitializationException>(Action);
            Assert.StartsWith($"Failed to load plugin '{_pluginAssembly}'.", ex.Message, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests from <seealso cref="ExpanderPluginLoader.LoadAllRegisteredPluginsAndBootstrap(App)"/> in happy flow.
        /// </summary>
        [Fact]
        public void LoadShouldVerify()
        {
            // arrange
            _mockedAssembly.Setup(x => x.GetExportedTypes()).Returns(new[] { _fakes.IExpanderDependencyManager.Object.GetType() });
            _mockedAssembly.Setup(x => x.GetReferencedAssemblies()).Returns(new[] { new AssemblyName(Resources.PackageAssemblyName) { Version = _version } });

            _fakes.IObjectActivator.Setup(x => x.CreateInstance(
                _fakes.IExpanderDependencyManager.Object.GetType(),
                _app.Expanders.First(),
                _fakes.IDependencyManager.Object))
                .Returns(_fakes.IExpanderDependencyManager.Object);

            // act
            _interactor.LoadAllRegisteredPluginsAndBootstrap(_app);

            // assert
            _fakes.IObjectActivator.Verify(x => x.CreateInstance(_fakes.IExpanderDependencyManager.Object.GetType(), _app.Expanders.First(), _fakes.IDependencyManager.Object), Times.Once);
            _fakes.IExpanderDependencyManager.Verify(x => x.Register(), Times.Once);
        }

        /// <summary>
        /// Tests from <seealso cref="ExpanderPluginLoader.LoadAllRegisteredPluginsAndBootstrap(App)"/> where the assembly version is incompatible.
        /// </summary>
        [Fact]
        public void LoadShouldThrowInitializationExceptionBecauseAssemblyVersionsAreIncompatible()
        {
            // arrange
            _mockedAssembly.Setup(x => x.GetExportedTypes()).Returns(new[] { _fakes.IExpanderDependencyManager.Object.GetType() });
            _mockedAssembly.Setup(x => x.GetReferencedAssemblies()).Returns(new[] { new AssemblyName(Resources.PackageAssemblyName) { Version = new Version("1.0.0.0") } });

            _fakes.IObjectActivator.Setup(x => x.CreateInstance(
                _fakes.IExpanderDependencyManager.Object.GetType(),
                _app.Expanders.First(),
                _fakes.IDependencyManager.Object))
                .Returns(_fakes.IExpanderDependencyManager.Object);

            // act & assert
            InitializationException exception = Assert.Throws<InitializationException>(() => _interactor.LoadAllRegisteredPluginsAndBootstrap(_app));
            Assert.StartsWith($"Failed to load plugin '{_pluginAssembly}'. LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Initializers.InitializationException: Incompatible versions used. Entry assembly version: {_version}, Plugin assembly version: 1.0.0.0", exception.Message, StringComparison.Ordinal);
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

            _mockedAssembly
                .Setup(x => x.GetExportedTypes())
                .Returns(new[] { mockedIExpander.Object.GetType() });

            _fakes.IDirectory
                .Setup(x => x.GetFiles(path, _searchPattern, SearchOption.AllDirectories))
                .Returns(new string[] { _pluginAssembly });

            _fakes.IAssemblyContext
                .Setup(x => x.Load(_pluginAssembly))
                .Returns(_mockedAssembly.Object);

            _fakes.IObjectActivator.Setup(x => x.CreateInstance(
                _fakes.IExpanderDependencyManager.Object.GetType()))
                .Returns(_fakes.IExpanderDependencyManager.Object);

            // act
            IEnumerable<IExpander> result = _interactor.ShallowLoadAllExpanders(path);

            // assert
            _fakes.IObjectActivator.Verify(x => x.CreateInstance(It.IsAny<Type>()), Times.Once);
            Assert.Single(result);
        }
    }
}
