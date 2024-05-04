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
        private readonly IGetRepository<App> gateway = dependencyFactory.Resolve<IGetRepository<App>>();
        private readonly GenerationOptions options = dependencyFactory.Resolve<GenerationOptions>();
        private readonly IExpanderPluginLoader pluginLoader = dependencyFactory.Resolve<IExpanderPluginLoader>();
        private readonly IDependencyManager dependencyManager = dependencyFactory.Resolve<IDependencyManager>();
        private readonly IDependencyFactory dependencyFactory = dependencyFactory;

        /// <inheritdoc/>
        public ICodeGenerator Build()
        {
            App app = gateway.GetById(options.AppId) ?? throw new CodeGenerationException($"No application model available with the provided Id {options.AppId}.");

            pluginLoader.LoadAllRegisteredPluginsAndBootstrap(app);
            dependencyManager.AddSingleton(app);
            dependencyManager.Build();

            return dependencyFactory.Resolve<ICodeGenerator>();
        }
    }
}
