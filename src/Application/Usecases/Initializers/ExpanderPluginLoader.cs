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
        private readonly string searchPattern = "*.Expanders.*.dll";
        private readonly GenerationOptions options;
        private readonly IDirectory directoryService;
        private readonly IAssemblyContext assemblyContext;
        private readonly ILogger logger;
        private readonly IObjectActivator activator;
        private readonly IDependencyManager dependencyManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpanderPluginLoader"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpanderPluginLoader(IDependencyFactory dependencyFactory)
        {
            options = dependencyFactory.Get<GenerationOptions>();
            directoryService = dependencyFactory.Get<IDirectory>();
            assemblyContext = dependencyFactory.Get<IAssemblyContext>();
            logger = dependencyFactory.Get<ILogger>();
            activator = dependencyFactory.Get<IObjectActivator>();
            dependencyManager = dependencyFactory.Get<IDependencyManager>();
        }

        /// <inheritdoc/>
        public void LoadAllRegisteredPluginsAndBootstrap(App app)
        {
            foreach (Expander expander in app.Expanders.Where(x => x.Enabled))
            {
                logger.Info($"===Loading Expander {expander.Name}===");

                string rootDirectory = Path.Combine(options.ExpandersFolder, expander.Name);
                string[] files = directoryService.GetFiles(rootDirectory, searchPattern, SearchOption.TopDirectoryOnly);
                if (!files.Any())
                {
                    throw new InitializationException($"No plugin assembly detected in '{rootDirectory}'. The plugin assembly should match the following '{searchPattern}' pattern");
                }

                LoadPlugins(files)
                    .ForEach(assembly => BootstrapPlugin(expander, assembly));

                logger.Info($"===End Loading Expander {expander.Name}===");
                logger.Trace(string.Empty);
            }
        }

        public List<IExpander> ShallowLoadAllExpanders(string path)
        {
            List<IExpander> result = new();

            string[] assemblyPaths = directoryService.GetFiles(path, searchPattern, SearchOption.AllDirectories);
            foreach (string assemblyPath in assemblyPaths)
            {
                Assembly assembly = LoadPlugin(assemblyPath);

                Type expanderType = assembly.GetExportedTypes()
                    .Where(x => x.IsClass && !x.IsAbstract)
                    .Single(x => x.GetInterfaces()
                    .Contains(typeof(IExpander)));

                IExpander expander = (IExpander)activator.CreateInstance(expanderType);
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
            Assembly assembly = assemblyContext.Load(assemblyFile);
            logger.Trace($"Plugin context {assemblyFile} has been successfully loaded...");
            return assembly;
        }

        private void BootstrapPlugin(Expander expander, Assembly assembly)
        {
            Type bootstrapperType = assembly.GetExportedTypes()
                .Where(x => x.IsClass && !x.IsAbstract)
                .Single(x => x.GetInterfaces()
                .Contains(typeof(IExpanderDependencyManager)));

            IExpanderDependencyManager expanderDependencyManager = (IExpanderDependencyManager)activator
                .CreateInstance(bootstrapperType, expander, dependencyManager);

            expanderDependencyManager.Register();
        }
    }
}
