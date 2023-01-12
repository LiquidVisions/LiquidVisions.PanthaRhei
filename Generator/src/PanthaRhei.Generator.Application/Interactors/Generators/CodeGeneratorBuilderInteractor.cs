using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Initializers;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Generators
{
    /// <summary>
    /// Implements the contract <seealso cref="ICodeGeneratorBuilderInteractor"/>.
    /// </summary>
    internal class CodeGeneratorBuilderInteractor : ICodeGeneratorBuilderInteractor
    {
        private readonly IGetGateway<App> gateway;
        private readonly Parameters parameters;
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
            parameters = dependencyFactory.Get<Parameters>();
            pluginLoader = dependencyFactory.Get<IExpanderPluginLoaderInteractor>();
            dependencyManager = dependencyFactory.Get<IDependencyManagerInteractor>();
            this.dependencyFactory = dependencyFactory;
        }

        /// <inheritdoc/>
        public ICodeGeneratorInteractor Build()
        {
            App app = gateway.GetById(parameters.AppId);
            if (app == null)
            {
                throw new CodeGenerationException($"No application model available with the provided Id {parameters.AppId}.");
            }

            pluginLoader.LoadAllRegisteredPluginsAndBootstrap(app);
            dependencyManager.AddSingleton(app);
            dependencyManager.Build();

            return dependencyFactory.Get<ICodeGeneratorInteractor>();
        }
    }
}
