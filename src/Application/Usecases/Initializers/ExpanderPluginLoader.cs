using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Initializers;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Initializers
{
    /// <summary>
    /// An implementation of <seealso cref="IExpanderPluginLoader"/>.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ExpanderPluginLoader"/> class.
    /// </remarks>
    /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
    internal class ExpanderPluginLoader(IDependencyFactory dependencyFactory) : IExpanderPluginLoader
    {
        private readonly string _searchPattern = "*.Expanders.*.dll";
        private readonly GenerationOptions _options = dependencyFactory.Resolve<GenerationOptions>();
        private readonly IDirectory _directoryService = dependencyFactory.Resolve<IDirectory>();
        private readonly IAssemblyContext _assemblyContext = dependencyFactory.Resolve<IAssemblyContext>();
        private readonly ILogger _logger = dependencyFactory.Resolve<ILogger>();
        private readonly IObjectActivator _activator = dependencyFactory.Resolve<IObjectActivator>();
        private readonly IDependencyManager _dependencyManager = dependencyFactory.Resolve<IDependencyManager>();
        private readonly IAssemblyProvider _assemblyProvider = dependencyFactory.Resolve<IAssemblyProvider>();

        /// <inheritdoc/>
        public void LoadAllRegisteredPluginsAndBootstrap(App app)
        {
            foreach (Expander expander in app.Expanders.Where(x => x.Enabled))
            {
                _logger.Info($"===Loading Expander {expander.Name}===");

                string rootDirectory = Path.Combine(_options.ExpandersFolder, expander.Name);
                string[] files = _directoryService.GetFiles(rootDirectory, _searchPattern, SearchOption.TopDirectoryOnly);
                if (files.Length == 0)
                {
                    throw new InitializationException($"No plugin assembly detected in '{rootDirectory}'. The plugin assembly should match the following '{_searchPattern}' pattern");
                }

                LoadPlugins(files)
                    .ForEach(assembly => BootstrapPlugin(expander, assembly));

                _logger.Info($"===End Loading Expander {expander.Name}===");
                _logger.Trace(string.Empty);
            }
        }

        public ICollection<IExpander> ShallowLoadAllExpanders(string path)
        {
            List<IExpander> result = [];

            string[] assemblyPaths = _directoryService.GetFiles(path, _searchPattern, SearchOption.AllDirectories);
            foreach (string assemblyPath in assemblyPaths)
            {
                Assembly assembly = LoadPlugin(assemblyPath);

                Type expanderType = assembly.GetExportedTypes()
                    .Where(x => x.IsClass && !x.IsAbstract)
                    .Single(x => x.GetInterfaces()
                    .Contains(typeof(IExpander)));

                IExpander expander = (IExpander)_activator.CreateInstance(expanderType);
                result.Add(expander);
            }

            return result;
        }

        private List<Assembly> LoadPlugins(string[] assemblyFiles)
        {
            List<Assembly> plugins = [];

            AssemblyName entryAssemblyName = _assemblyProvider.EntryAssembly.GetName();
            _logger.Info($"Loading plugins for with compatibility version {entryAssemblyName.Version}...");

            foreach (string assemblyFile in assemblyFiles)
            {
                try
                {
                    Assembly assembly = LoadPlugin(assemblyFile);
                    AssemblyName pluginAssemblyName = assembly.GetReferencedAssemblies().Single(x => x.Name == Resources.PackageAssemblyName);
                    _logger.Info($"Attempting to load plugin {pluginAssemblyName.Name} with compatibility version {pluginAssemblyName.Version}...");

                    ValidateAssemblyVersion(entryAssemblyName, pluginAssemblyName);

                    plugins.Add(assembly);
                }
                catch (Exception innerException)
                {
                    throw new InitializationException($"Failed to load plugin '{assemblyFile}'. {innerException}", innerException);
                }
            }

            return plugins;
        }

        private Assembly LoadPlugin(string assemblyFile)
        {
            Assembly assembly = LoadAssembly(assemblyFile);
            _logger.Trace($"Plugin context {assemblyFile} has been successfully loaded...");
            return assembly;
        }

        private Assembly LoadAssembly(string assemblyFile)
        {
            return _assemblyContext.Load(assemblyFile);
        }

        private static void ValidateAssemblyVersion(AssemblyName entryAssembly, AssemblyName pluginAssembly)
        {
            Version entryAssemblyVersion = entryAssembly.Version;
            Version pluginAssemblyVersion = pluginAssembly.Version;

            bool incompatibleVersionsUsed = entryAssemblyVersion != pluginAssemblyVersion;
            if (incompatibleVersionsUsed)
            {
                string message = $"Incompatible versions used. Entry assembly version: {entryAssemblyVersion}, Plugin assembly version: {pluginAssemblyVersion}";
                throw new InitializationException(message);
            }
        }

        private void BootstrapPlugin(Expander expander, Assembly assembly)
        {
            Type bootstrapperType = assembly.GetExportedTypes()
                .Where(x => x.IsClass && !x.IsAbstract)
                .Single(x => x.GetInterfaces()
                .Contains(typeof(IExpanderDependencyManager)));

            IExpanderDependencyManager expanderDependencyManager = (IExpanderDependencyManager)_activator
                .CreateInstance(bootstrapperType, expander, _dependencyManager);

            expanderDependencyManager.Register();
        }
    }
}
