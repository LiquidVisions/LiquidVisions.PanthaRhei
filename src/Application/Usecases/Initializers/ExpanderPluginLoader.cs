using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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
            _options = dependencyFactory.Resolve<GenerationOptions>();
            _directoryService = dependencyFactory.Resolve<IDirectory>();
            _assemblyContext = dependencyFactory.Resolve<IAssemblyContext>();
            _logger = dependencyFactory.Resolve<ILogger>();
            _activator = dependencyFactory.Resolve<IObjectActivator>();
            _dependencyManager = dependencyFactory.Resolve<IDependencyManager>();
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

        public ICollection<IExpander> ShallowLoadAllExpanders(string path)
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
            Assembly assembly = LoadAssembly(assemblyFile);
            ValidateAssemblyVersion(assembly, assemblyFile);
            _logger.Trace($"Plugin context {assemblyFile} has been successfully loaded...");
            return assembly;
        }

        private Assembly LoadAssembly(string assemblyFile)
        {
            return _assemblyContext.Load(assemblyFile);
        }

        private void ValidateAssemblyVersion(Assembly assembly, string assemblyFile)
        {
            string currentVersion = CheckAssemblyVersion(Assembly.GetEntryAssembly());
            string pluginVersion = GetPluginVersion(assembly);

            string incompatibilityMessage = GenerateIncompatibleVersionMessage(assemblyFile, currentVersion, pluginVersion);
            _logger.Info(incompatibilityMessage);

            bool incompatibleVersionsUsed = currentVersion != pluginVersion;
            if (incompatibleVersionsUsed)
            {
                throw new InitializationException(GenerateIncompatibleVersionMessage(assemblyFile, currentVersion, pluginVersion));
            }
        }

        private string GetPluginVersion(Assembly assembly)
        {
            Assembly pluginAssembly = _assemblyContext.Load(assembly.GetReferencedAssemblies().Single(x => x.Name == Resources.PackageAssemblyName));
            return CheckAssemblyVersion(pluginAssembly);
        }

        private string GenerateIncompatibleVersionMessage(string assemblyFile, string currentVersion, string pluginVersion)
        {
            StringBuilder sb = new();
            sb.AppendLine(CultureInfo.CurrentCulture, $"Plugin '{assemblyFile}' has an incompatible version.");
            sb.AppendLine(CultureInfo.CurrentCulture, $"Current version: {currentVersion}");
            sb.AppendLine(CultureInfo.CurrentCulture, $"Plugin version: {pluginVersion}");
            return sb.ToString();
        }

        private string CheckAssemblyVersion(Assembly assembly)
        {
            return assembly?
                .GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false)
                .OfType<AssemblyInformationalVersionAttribute>()
                .SingleOrDefault()?
                .InformationalVersion
                .ToUpperInvariant();
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
