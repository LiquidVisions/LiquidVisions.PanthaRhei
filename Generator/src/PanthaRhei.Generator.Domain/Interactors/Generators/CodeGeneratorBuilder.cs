using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Initializers;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators
{
    /// <summary>
    /// Implements the contract <seealso cref="ICodeGeneratorBuilderInteractor"/>.
    /// </summary>
    internal class CodeGeneratorBuilder : ICodeGeneratorBuilderInteractor
    {
        private readonly IGenericRepository<App> appRepository;
        private readonly Parameters parameters;
        private readonly IExpanderPluginLoader pluginLoader;
        private readonly IDependencyManagerInteractor dependencyManager;
        private readonly IDependencyFactoryInteractor dependencyFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGeneratorBuilder"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public CodeGeneratorBuilder(IDependencyFactoryInteractor dependencyFactory)
        {
            appRepository = dependencyFactory.Get<IGenericRepository<App>>();
            parameters = dependencyFactory.Get<Parameters>();
            pluginLoader = dependencyFactory.Get<IExpanderPluginLoader>();
            dependencyManager = dependencyFactory.Get<IDependencyManagerInteractor>();
            this.dependencyFactory = dependencyFactory;
        }

        /// <inheritdoc/>
        public ICodeGeneratorInteractor Build()
        {
            App app = appRepository.GetById(parameters.AppId);
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
