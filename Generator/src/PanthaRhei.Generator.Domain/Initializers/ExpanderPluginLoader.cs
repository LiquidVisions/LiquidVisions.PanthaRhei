using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Initializers
{
    /// <summary>
    /// An implementation of <seealso cref="IExpanderPluginLoader"/>.
    /// </summary>
    internal class ExpanderPluginLoader : IExpanderPluginLoader
    {
        private readonly string searchPattern = "*.Expander.*.dll";
        private readonly Parameters parameters;
        private readonly IDirectoryService directoryService;
        private readonly IAssemblyContext assemblyContext;
        private readonly ILogger logger;
        private readonly IObjectActivator activator;
        private readonly IDependencyManager dependencyManager;
        private readonly IAssemblyManager assemblyManager;

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
            assemblyManager = dependencyResolver.Get<IAssemblyManager>();
        }

        /// <inheritdoc/>
        public void Load(App app)
        {
            foreach (Expander expander in app.Expanders)
            {
                logger.Info($"===Loading Expander {expander.Name}===");

                LoadExpanderPlugin(expander);

                logger.Info($"===End Loading Expander {expander.Name}===");
                logger.Trace(string.Empty);
            }
        }

        private void LoadExpanderPlugin(Expander expander)
        {
            string rootDirectory = System.IO.Path.Combine(parameters.ExpandersFolder, expander.Name);
            IEnumerable<string> assemblyFiles = directoryService.GetFiles(rootDirectory, searchPattern, System.IO.SearchOption.TopDirectoryOnly);
            if (!assemblyFiles.Any())
            {
                throw new InitializationException($"No plugin assembly detected in '{rootDirectory}'. The plugin assembly should match the following '{searchPattern}' pattern");
            }

            foreach (string assemblyFile in assemblyFiles)
            {
                try
                {
                    Assembly assembly = assemblyContext.Load(assemblyFile);
                    logger.Trace($"Plugin context {assemblyFile} has been successfully loaded...");

                    Type bootstrapperType = assembly.GetExportedTypes()
                        .Where(x => x.IsClass && !x.IsAbstract)
                        .Single(x => x.GetInterfaces().Contains(typeof(IExpanderDependencyManager)));

                    IExpanderDependencyManager expanderDependencyManager = (IExpanderDependencyManager)activator
                        .CreateInstance(bootstrapperType, expander, dependencyManager, logger, assemblyManager);

                    expanderDependencyManager.Register();
                }
                catch (Exception innerException)
                {
                    throw new InitializationException($"Failed to load plugin '{assemblyFile}'.", innerException);
                }
            }
        }
    }
}
