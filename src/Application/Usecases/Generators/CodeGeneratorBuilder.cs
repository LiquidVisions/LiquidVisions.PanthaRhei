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
        private readonly IGetRepository<App> gateway;
        private readonly GenerationOptions options;
        private readonly IExpanderPluginLoader pluginLoader;
        private readonly IDependencyManager dependencyManager;
        private readonly IDependencyFactory dependencyFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGeneratorBuilder"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public CodeGeneratorBuilder(IDependencyFactory dependencyFactory)
        {
            gateway = dependencyFactory.Get<IGetRepository<App>>();
            options = dependencyFactory.Get<GenerationOptions>();
            pluginLoader = dependencyFactory.Get<IExpanderPluginLoader>();
            dependencyManager = dependencyFactory.Get<IDependencyManager>();
            this.dependencyFactory = dependencyFactory;
        }

        /// <inheritdoc/>
        public ICodeGenerator Build()
        {
            App app = gateway.GetById(options.AppId) ?? throw new CodeGenerationException($"No application model available with the provided Id {options.AppId}.");

            pluginLoader.LoadAllRegisteredPluginsAndBootstrap(app);
            dependencyManager.AddSingleton(app);
            dependencyManager.Build();

            return dependencyFactory.Get<ICodeGenerator>();
        }
    }
}
