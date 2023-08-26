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
    internal class ExpanderPluginLoader : IExpanderPluginLoader
    {
        private readonly string _searchPattern = "*.Expanders.*.dll";
        private readonly GenerationOptions _options;
        private readonly IDirectory _directoryService;
        private readonly IAssemblyContext _assemblyContext;
        private readonly ILogger _logger;
        private readonly IObjectActivator _activator;
        private readonly IDependencyManager _dependencyManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpanderPluginLoader"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpanderPluginLoader(IDependencyFactory dependencyFactory)
        {
            _options = dependencyFactory.Get<GenerationOptions>();
            _directoryService = dependencyFactory.Get<IDirectory>();
            _assemblyContext = dependencyFactory.Get<IAssemblyContext>();
            _logger = dependencyFactory.Get<ILogger>();
            _activator = dependencyFactory.Get<IObjectActivator>();
            _dependencyManager = dependencyFactory.Get<IDependencyManager>();
        }

        /// <inheritdoc/>
        public void LoadAllRegisteredPluginsAndBootstrap(App app)
        {
            foreach (Expander expander in app.Expanders.Where(x => x.Enabled))
            {
                _logger.Info($"===Loading Expander {expander.Name}===");

                string rootDirectory = Path.Combine(_options.ExpandersFolder, expander.Name);
                string[] files = _directoryService.GetFiles(rootDirectory, _searchPattern, SearchOption.TopDirectoryOnly);
                if (!files.Any())
                {
                    throw new InitializationException($"No plugin assembly detected in '{rootDirectory}'. The plugin assembly should match the following '{_searchPattern}' pattern");
                }

                LoadPlugins(files)
                    .ForEach(assembly => BootstrapPlugin(expander, assembly));

                _logger.Info($"===End Loading Expander {expander.Name}===");
                _logger.Trace(string.Empty);
            }
        }

        public List<IExpander> ShallowLoadAllExpanders(string path)
        {
            List<IExpander> result = new();

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
            List<Assembly> plugins = new();

            foreach (string assemblyFile in assemblyFiles)
            {
                try
                {
                    Assembly assembly = LoadPlugin(assemblyFile);
                    plugins.Add(assembly);
                }
                catch (Exception innerException)
                {
                    throw new InitializationException($"Failed to load plugin '{assemblyFile}'.", innerException);
                }
            }

            return plugins;
        }

        private Assembly LoadPlugin(string assemblyFile)
        {
            Assembly assembly = _assemblyContext.Load(assemblyFile);
            _logger.Trace($"Plugin context {assemblyFile} has been successfully loaded...");
            return assembly;
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
