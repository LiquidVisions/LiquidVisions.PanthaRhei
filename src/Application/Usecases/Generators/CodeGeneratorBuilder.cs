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
    /// <remarks>
    /// Initializes a new instance of the <see cref="CodeGeneratorBuilder"/> class.
    /// </remarks>
    /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
    internal class CodeGeneratorBuilder(IDependencyFactory dependencyFactory) : ICodeGeneratorBuilder
    {
        private readonly IGetRepository<App> _gateway = dependencyFactory.Resolve<IGetRepository<App>>();
        private readonly GenerationOptions _options = dependencyFactory.Resolve<GenerationOptions>();
        private readonly IExpanderPluginLoader _pluginLoader = dependencyFactory.Resolve<IExpanderPluginLoader>();
        private readonly IDependencyManager _dependencyManager = dependencyFactory.Resolve<IDependencyManager>();
        private readonly IDependencyFactory _dependencyFactory = dependencyFactory;

        /// <inheritdoc/>
        public ICodeGenerator Build()
        {
            App app = _gateway.GetById(_options.AppId) ?? throw new CodeGenerationException($"No application model available with the provided Id {_options.AppId}.");

            _pluginLoader.LoadAllRegisteredPluginsAndBootstrap(app);
            _dependencyManager.AddSingleton(app);
            _dependencyManager.Build();

            return _dependencyFactory.Resolve<ICodeGenerator>();
        }
    }
}
