using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Initializers;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Generators
{
    /// <summary>
    /// Implements the contract <seealso cref="ICodeGeneratorBuilder"/>.
    /// </summary>
    internal class CodeGeneratorBuilder : ICodeGeneratorBuilder
    {
        private readonly IAppRepository appContext;
        private readonly Parameters parameters;
        private readonly IExpanderPluginLoader pluginLoader;
        private readonly IDependencyManager dependencyManager;
        private readonly IDependencyResolver dependencyResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGeneratorBuilder"/> class.
        /// </summary>
        /// <param name="dependencyResolver"><seealso cref="IDependencyResolver"/></param>
        public CodeGeneratorBuilder(IDependencyResolver dependencyResolver)
        {
            appContext = dependencyResolver.Get<IAppRepository>();
            parameters = dependencyResolver.Get<Parameters>();
            pluginLoader = dependencyResolver.Get<IExpanderPluginLoader>();
            dependencyManager = dependencyResolver.Get<IDependencyManager>();
            this.dependencyResolver = dependencyResolver;
        }

        /// <inheritdoc/>
        public ICodeGenerator Build()
        {
            App app = appContext.GetById(parameters.AppId);
            if (app == null)
            {
                throw new CodeGenerationException($"No application model available with the provided Id {parameters.AppId}.");
            }

            pluginLoader.Load(app);
            dependencyManager.AddSingleton(app);
            dependencyManager.Build();

            return dependencyResolver.Get<ICodeGenerator>();
        }
    }
}
