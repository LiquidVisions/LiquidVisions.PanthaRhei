using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LiquidVisions.PanthaRhei.Application.Usecases.Initializers;
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
        private readonly Mock<Assembly> _mockedAssembly = new();
        private readonly App _app;

        /// <summary>
        /// INitializes a new instance of the <see cref="ExpanderPluginLoaderTests"/> class.
        /// </summary>
        public ExpanderPluginLoaderTests()
        {
            _app = new() { Expanders = new List<Expander> { new Expander() { Name = _expanderName } } };

            _fakes.IAssemblyContext.Setup(x => x.Load(_pluginAssembly)).Returns(_mockedAssembly.Object);

            _interactor = new ExpanderPluginLoader(_fakes.IDependencyFactory.Object);

            _fakes.IFile.Setup(x => x.GetDirectory(_fakes.GenerationOptions.Object.ExpandersFolder)).Returns(@"C:\Some\Fake\");
            _fakes.IAssemblyContext.Setup(x => x.Load(_pluginAssembly)).Returns(_mockedAssembly.Object);
            _fakes.IDirectory.Setup(x => x.GetFiles(Path.Combine(_fakes.GenerationOptions.Object.ExpandersFolder, _expanderName), _searchPattern, SearchOption.TopDirectoryOnly)).Returns(new string[] { _pluginAssembly });
        }

        /// <summary>
        /// Tests for <seealso cref="ExpanderPluginLoader.LoadAllRegisteredPluginsAndBootstrap(App)"/> while throwing <seealso cref="InitializationException"/>.
        /// </summary>
        [Fact]
        public void Load_RootFolderDoesNotContainPluginAssemblies_ShouldThrowException()
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
        public void Load_LoadAssemblyFilesThrowsException_ShouldRethrowWithMessage()
        {
            // arrange
            _fakes.IAssemblyContext.Setup(x => x.Load(_pluginAssembly)).Throws<Exception>();

            // act
            void Action() => _interactor.LoadAllRegisteredPluginsAndBootstrap(_app);

            // assert
            InitializationException ex = Assert.Throws<InitializationException>(Action);
            Assert.Equal($"Failed to load plugin '{_pluginAssembly}'.", ex.Message);
        }

        /// <summary>
        /// Tests from <seealso cref="ExpanderPluginLoader.LoadAllRegisteredPluginsAndBootstrap(App)"/> in happy flow.
        /// </summary>
        [Fact]
        public void Load_ShouldVerify()
        {
            // arrange
            _mockedAssembly.Setup(x => x.GetExportedTypes()).Returns(new[] { _fakes.IExpanderDependencyManager.Object.GetType() });

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
        /// Tests for <seealso cref="ExpanderPluginLoader.ShallowLoadAllExpanders(string)"/>.
        /// </summary>
        [Fact]
        public void ShallowLoad_ShouldVerify()
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
            List<IExpander> result = _interactor.ShallowLoadAllExpanders(path);

            // assert
            _fakes.IObjectActivator.Verify(x => x.CreateInstance(It.IsAny<Type>()), Times.Once);
            Assert.Single(result);
        }
    }
}
