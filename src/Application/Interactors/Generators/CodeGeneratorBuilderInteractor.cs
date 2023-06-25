using LiquidVisions.PanthaRhei.Application.Interactors.Initializers;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Gateways;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Application.Interactors.Generators
{
    /// <summary>
    /// Implements the contract <seealso cref="ICodeGeneratorBuilderInteractor"/>.
    /// </summary>
    internal class CodeGeneratorBuilderInteractor : ICodeGeneratorBuilderInteractor
    {
        private readonly IGetGateway<App> gateway;
        private readonly GenerationOptions options;
        private readonly IExpanderPluginLoaderInteractor pluginLoader;
        private readonly IDependencyManagerInteractor dependencyManager;
        private readonly IDependencyFactoryInteractor dependencyFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGeneratorBuilderInteractor"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public CodeGeneratorBuilderInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            gateway = dependencyFactory.Get<IGetGateway<App>>();
            options = dependencyFactory.Get<GenerationOptions>();
            pluginLoader = dependencyFactory.Get<IExpanderPluginLoaderInteractor>();
            dependencyManager = dependencyFactory.Get<IDependencyManagerInteractor>();
            this.dependencyFactory = dependencyFactory;
        }

        /// <inheritdoc/>
        public ICodeGeneratorInteractor Build()
        {
            App app = gateway.GetById(options.AppId) ?? throw new CodeGenerationException($"No application model available with the provided Id {options.AppId}.");

            pluginLoader.LoadAllRegisteredPluginsAndBootstrap(app);
            dependencyManager.AddSingleton(app);
            dependencyManager.Build();

            return dependencyFactory.Get<ICodeGeneratorInteractor>();
        }
    }
}
