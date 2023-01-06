using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Initializers;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Initializers
{
    /// <summary>
    /// An implementation of <seealso cref="IExpanderPluginLoaderInteractor"/>.
    /// </summary>
    internal class ExpanderPluginLoaderInteractor : IExpanderPluginLoaderInteractor
    {
        private readonly string searchPattern = "*.Expanders.*.dll";
        private readonly Parameters parameters;
        private readonly IDirectory directoryService;
        private readonly IAssemblyContextInteractor assemblyContext;
        private readonly ILogger logger;
        private readonly IObjectActivatorInteractor activator;
        private readonly IDependencyManagerInteractor dependencyManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpanderPluginLoaderInteractor"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ExpanderPluginLoaderInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            parameters = dependencyFactory.Get<Parameters>();
            directoryService = dependencyFactory.Get<IDirectory>();
            assemblyContext = dependencyFactory.Get<IAssemblyContextInteractor>();
            logger = dependencyFactory.Get<ILogger>();
            activator = dependencyFactory.Get<IObjectActivatorInteractor>();
            dependencyManager = dependencyFactory.Get<IDependencyManagerInteractor>();
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

        public List<IExpanderInteractor> ShallowLoadAllExpanders(string path)
        {
            List<IExpanderInteractor> result = new();

            string[] assemblyPaths = directoryService.GetFiles(path, searchPattern, SearchOption.AllDirectories);
            foreach (string assemblyPath in assemblyPaths)
            {
                Assembly assembly = LoadPlugin(assemblyPath);
                Type expanderType = assembly.GetExportedTypes()
                    .Where(x => x.IsClass && !x.IsAbstract)
                    .Single(x => x.GetInterfaces().Contains(typeof(IExpanderInteractor)));

                IExpanderInteractor expander = (IExpanderInteractor)activator.CreateInstance(expanderType);
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
                .Single(x => x.GetInterfaces().Contains(typeof(IExpanderDependencyManagerInteractor)));

            IExpanderDependencyManagerInteractor expanderDependencyManager = (IExpanderDependencyManagerInteractor)activator
                .CreateInstance(bootstrapperType, expander, dependencyManager);

            expanderDependencyManager.Register();
        }
    }
}
