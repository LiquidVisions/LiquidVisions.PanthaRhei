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
    public class ExpanderPluginLoaderTests
    {
        private readonly string searchPattern = "*.Expanders.*.dll";
        private readonly string expanderName = "ExpanderName";
        private readonly string pluginAssembly = @"C:\Some\Fake\Plugin.Expanders.Assembly.dll";

        private readonly ApplicationFakes fakes = new();
        private readonly ExpanderPluginLoader interactor;
        private readonly Mock<Assembly> mockedAssembly = new();
        private readonly App app;

        public ExpanderPluginLoaderTests()
        {
            app = new() { Expanders = new List<Expander> { new Expander() { Name = expanderName } } };

            fakes.IAssemblyContext.Setup(x => x.Load(pluginAssembly)).Returns(mockedAssembly.Object);

            interactor = new ExpanderPluginLoader(fakes.IDependencyFactory.Object);

            fakes.IFile.Setup(x => x.GetDirectory(fakes.GenerationOptions.Object.ExpandersFolder)).Returns(@"C:\Some\Fake\");
            fakes.IAssemblyContext.Setup(x => x.Load(pluginAssembly)).Returns(mockedAssembly.Object);
            fakes.IDirectory.Setup(x => x.GetFiles(Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, expanderName), searchPattern, SearchOption.TopDirectoryOnly)).Returns(new string[] { pluginAssembly });
        }

        [Fact]
        public void Load_RootFolderDoesNotContainPluginAssemblies_ShouldThrowException()
        {
            // arrange
            fakes.IDirectory.Setup(x => x.GetFiles(Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, expanderName), searchPattern, SearchOption.TopDirectoryOnly)).Returns(Array.Empty<string>());

            // act
            void Action() => interactor.LoadAllRegisteredPluginsAndBootstrap(app);

            // assert
            InitializationException ex = Assert.Throws<InitializationException>(Action);
            Assert.Equal($"No plugin assembly detected in '{Path.Combine(fakes.GenerationOptions.Object.ExpandersFolder, expanderName)}'. The plugin assembly should match the following '{searchPattern}' pattern", ex.Message);
        }

        [Fact]
        public void Load_LoadAssemblyFilesThrowsException_ShouldRethrowWithMessage()
        {
            // arrange
            fakes.IAssemblyContext.Setup(x => x.Load(pluginAssembly)).Throws<Exception>();

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
            mockedAssembly.Setup(x => x.GetExportedTypes()).Returns(new[] { fakes.IExpanderDependencyManager.Object.GetType() });

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

        [Fact]
        public void ShallowLoad_ShouldVerify()
        {
            // arrange
            string path = "C:\\Some\\Fake\\";

            Mock<IExpander> mockedIExpander = new();

            mockedAssembly
                .Setup(x => x.GetExportedTypes())
                .Returns(new[] { mockedIExpander.Object.GetType() });

            fakes.IDirectory
                .Setup(x => x.GetFiles(path, searchPattern, SearchOption.AllDirectories))
                .Returns(new string[] { pluginAssembly });

            fakes.IAssemblyContext
                .Setup(x => x.Load(pluginAssembly))
                .Returns(mockedAssembly.Object);

            fakes.IObjectActivator.Setup(x => x.CreateInstance(
                fakes.IExpanderDependencyManager.Object.GetType()))
                .Returns(fakes.IExpanderDependencyManager.Object);

            // act
            List<IExpander> result = interactor.ShallowLoadAllExpanders(path);

            // assert
            fakes.IObjectActivator.Verify(x => x.CreateInstance(It.IsAny<Type>()), Times.Once);
            Assert.Single(result);
        }
    }
}
