using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Initializers
{
    /// <summary>
    /// An implementation of <seealso cref="IExpanderPluginLoader"/>.
    /// </summary>
    internal class ExpanderPluginLoader : IExpanderPluginLoader
    {
        private readonly string searchPattern = "*.Expanders.*.dll";
        private readonly Parameters parameters;
        private readonly IDirectoryService directoryService;
        private readonly IAssemblyContext assemblyContext;
        private readonly ILogger logger;
        private readonly IObjectActivator activator;
        private readonly IDependencyManager dependencyManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpanderPluginLoader"/> class.
        /// </summary>
        /// <param name="dependencyResolver"><seealso cref="IDependencyResolver"/></param>
        public ExpanderPluginLoader(IDependencyResolver dependencyResolver)
        {
            parameters = dependencyResolver.Get<Parameters>();
            directoryService = dependencyResolver.Get<IDirectoryService>();
            assemblyContext = dependencyResolver.Get<IAssemblyContext>();
            logger = dependencyResolver.Get<ILogger>();
            activator = dependencyResolver.Get<IObjectActivator>();
            dependencyManager = dependencyResolver.Get<IDependencyManager>();
        }

        /// <inheritdoc/>
        public void LoadAllRegisteredPluginsAndBootstrap(App app)
        {
            foreach (Expander expander in app.Expanders)
            {
                logger.Info($"===Loading Expander {expander.Name}===");

                string rootDirectory = Path.Combine(parameters.ExpandersFolder, expander.Name);
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
                    .Single(x => x.GetInterfaces().Contains(typeof(IExpander)));

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
                .Single(x => x.GetInterfaces().Contains(typeof(IExpanderDependencyManager)));

            IExpanderDependencyManager expanderDependencyManager = (IExpanderDependencyManager)activator
                .CreateInstance(bootstrapperType, expander, dependencyManager);

            expanderDependencyManager.Register();
        }
    }
}
