using LiquidVisions.PanthaRhei.Application.Usecases.Initializers;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Generators
{
    /// <summary>
    /// Implements the contract <seealso cref="ICodeGeneratorBuilder"/>.
    /// </summary>
    internal class CodeGeneratorBuilder : ICodeGeneratorBuilder
    {
        private readonly IGetRepository<App> _gateway;
        private readonly GenerationOptions _options;
        private readonly IExpanderPluginLoader _pluginLoader;
        private readonly IDependencyManager _dependencyManager;
        private readonly IDependencyFactory _dependencyFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGeneratorBuilder"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public CodeGeneratorBuilder(IDependencyFactory dependencyFactory)
        {
            _gateway = dependencyFactory.Get<IGetRepository<App>>();
            _options = dependencyFactory.Get<GenerationOptions>();
            _pluginLoader = dependencyFactory.Get<IExpanderPluginLoader>();
            _dependencyManager = dependencyFactory.Get<IDependencyManager>();
            _dependencyFactory = dependencyFactory;
        }

        /// <inheritdoc/>
        public ICodeGenerator Build()
        {
            App app = _gateway.GetById(_options.AppId) ?? throw new CodeGenerationException($"No application model available with the provided Id {_options.AppId}.");

            _pluginLoader.LoadAllRegisteredPluginsAndBootstrap(app);
            _dependencyManager.AddSingleton(app);
            _dependencyManager.Build();

            return _dependencyFactory.Get<ICodeGenerator>();
        }
    }
}
