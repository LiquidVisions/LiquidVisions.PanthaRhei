using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Harvesters;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.PostProcessors;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Preprocessors;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Rejuvenator;
using LiquidVisions.PanthaRhei.Generator.Domain.Initializers;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Expanders
{
    /// <summary>
    /// Represents an abstract implementation of <seealso cref="IExpanderDependencyManager"/> that allows dependency registration as part of a <seealso cref="IExpander"/>.
    /// </summary>
    /// <typeparam name="TExpander"><seealso cref="IExpander"/></typeparam>
    public abstract class AbstractExpanderDependencyManager<TExpander> : IExpanderDependencyManager
        where TExpander : class, IExpander
    {
        private readonly Expander expander;
        private readonly IDependencyManager dependencyManager;
        private readonly ILogger logger;
        private readonly IAssemblyManager assemblyManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractExpanderDependencyManager{TExpander}"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="Model"/></param>
        /// <param name="dependencyManager"><seealso cref="IDependencyResolver"/></param>
        protected AbstractExpanderDependencyManager(Expander expander, IDependencyManager dependencyManager)
        {
            this.dependencyManager = dependencyManager;
            IDependencyResolver dependencyResolver = this.dependencyManager.Build();

            this.expander = expander;
            logger = dependencyResolver.Get<ILogger>();
            assemblyManager = dependencyResolver.Get<IAssemblyManager>();
        }

        /// <summary>
        /// Gets the <seealso cref="IDependencyManager"/>.
        /// </summary>
        [ExcludeFromCodeCoverage]
        protected IDependencyManager DependencyManager => dependencyManager;

        /// <summary>
        /// Gets the <seealso cref="Model"/>.
        /// </summary>
        [ExcludeFromCodeCoverage]
        protected Expander Model => expander;

        /// <summary>
        /// Gets the <seealso cref="ILogger"/>.
        /// </summary>
        [ExcludeFromCodeCoverage]
        protected ILogger Logger => logger;

        /// <inheritdoc/>
        public virtual void Register()
        {
            Assembly assembly = assemblyManager.GetAssembly(GetType());

            RegisterPreProcessors(assembly);
            RegisterExpander(assembly);
            RegisterHandlers(assembly);
            RegisterHarvesters(assembly);
            RegisterRejuvenators(assembly);
            RegisterPostProcessors(assembly);
        }

        /// <summary>
        /// Registers all <seealso cref="Rejuvenator{TExpander}">Rejuvenators</seealso>.
        /// </summary>
        /// <param name="assembly">The <seealso cref="Assembly"/> that contain the <seealso cref="Rejuvenator{TExpander}">types</seealso> that should be registered.</param>
        public virtual void RegisterRejuvenators(Assembly assembly)
        {
            dependencyManager.AddTransient(typeof(IRejuvenator<TExpander>), typeof(RegionRejuvenator<TExpander>));
            logger.Debug($"Registered rejuvenator {typeof(IRejuvenator<TExpander>)} to match {nameof(RegionRejuvenator<TExpander>)} in the dependency container.");

            RegisterFromAssembly(assembly, typeof(IRejuvenator<TExpander>));
        }

        /// <summary>
        /// Register all types of a certain type that is part of the <seealso cref="Assembly"/>.
        /// </summary>
        /// <param name="assembly">The <seealso cref="Assembly"/> that contain the <seealso cref="Type">types</seealso> that should be registered.</param>
        /// <param name="serviceType">The type that should be registered.</param>
        public virtual void RegisterFromAssembly(Assembly assembly, Type serviceType)
        {
            var list = assembly
                .GetExportedTypes()
                .Where(x => x.GetInterfaces().Contains(serviceType));

            foreach (var type in list)
            {
                dependencyManager.AddTransient(serviceType, type);
                logger.Debug($"Registered {serviceType} to match {type.Name} in the dependency container.");
            }
        }

        /// <summary>
        /// Registers all <seealso cref="Harvester{TExpander}">Harvesters</seealso>.
        /// </summary>
        /// <param name="assembly">The <seealso cref="Assembly"/> that contain the <seealso cref="Harvester{TExpander}">types</seealso> that should be registered.</param>
        public virtual void RegisterHarvesters(Assembly assembly)
        {
            dependencyManager.AddTransient(typeof(IHarvester<TExpander>), typeof(RegionHarvester<TExpander>));
            logger.Debug($"Registered harvester {typeof(IHarvester<TExpander>)} to match {nameof(RegionHarvester<TExpander>)} in the dependency container.");

            RegisterFromAssembly(assembly, typeof(IHarvester<TExpander>));
        }

        /// <summary>
        /// Registers all <seealso cref="PostProcessor{TExpander}">PostProcessor</seealso>.
        /// </summary>
        /// <param name="assembly">The <seealso cref="Assembly"/> that contain the <seealso cref="PostProcessor{TExpander}">types</seealso> that should be registered.</param>
        public virtual void RegisterPostProcessors(Assembly assembly)
        {
            logger.Debug($"Registering {nameof(UnInstallDotNetTemplate<TExpander>)} as a {nameof(IPostProcessor<TExpander>)}");
            dependencyManager.AddTransient(typeof(IPostProcessor<TExpander>), typeof(UnInstallDotNetTemplate<TExpander>));

            RegisterFromAssembly(assembly, typeof(IPostProcessor<TExpander>));
        }

        /// <summary>
        /// Registers all <seealso cref="PreProcessor{TExpander}">PreProcessors</seealso>.
        /// </summary>
        /// <param name="assembly">The <seealso cref="Assembly"/> that contain the <seealso cref="PreProcessor{TExpander}">types</seealso> that should be registered.</param>
        public virtual void RegisterPreProcessors(Assembly assembly)
        {
            logger.Debug($"Registering {nameof(InstallDotNetTemplate<TExpander>)} as a {nameof(IPreProcessor<TExpander>)}");
            dependencyManager.AddTransient(typeof(IPreProcessor<TExpander>), typeof(InstallDotNetTemplate<TExpander>));

            RegisterFromAssembly(assembly, typeof(IPreProcessor<TExpander>));
        }

        /// <summary>
        /// Register all <seealso cref="IHandler{TExpander}"/> that are loaded in the <paramref name="assembly"/>.
        /// </summary>
        /// <param name="assembly"><seealso cref="Assembly"/></param>
        public virtual void RegisterHandlers(Assembly assembly)
        {
            var listOfHandlers = assembly
                .GetExportedTypes()
                .Where(x => x.IsClass && !x.IsAbstract)
                .Where(x => x.GetInterfaces().Contains(typeof(IHandler<TExpander>)))
                .ToList();

            if (!listOfHandlers.Any())
            {
                logger.Warn($"Expander '{expander.Name}' does not have any {nameof(IHandler<IExpander>)} implememntations.");
                return;
            }

            foreach (Type handlerType in listOfHandlers)
            {
                dependencyManager.AddTransient(typeof(IHandler<TExpander>), handlerType);
                logger.Trace($"Registered {typeof(IHandler<TExpander>)} to match {handlerType} in the dependency container.");
            }
        }

        /// <summary>
        /// Register all <seealso cref="IExpander"/> that are loaded in the <paramref name="assembly"/>.
        /// </summary>
        /// <param name="assembly"><seealso cref="Assembly"/></param>
        public virtual void RegisterExpander(Assembly assembly)
        {
            try
            {
                Type expanderType = assembly.GetExportedTypes()
                    .Where(x => x.IsClass && !x.IsAbstract)
                    .Single(x => x.GetInterfaces().Contains(typeof(IExpander)));

                dependencyManager.AddTransient(typeof(IExpander), expanderType);
                dependencyManager.AddTransient(expanderType, expanderType);
                logger.Trace($"Registered {expanderType} to match {typeof(IExpander)} in the dependency container.");
            }
            catch (InvalidOperationException exception)
            {
                throw new InitializationException($"Unable to load plugin '{expander.Name}'. No valid {nameof(IExpander)} derivatives found. The derivatives should be a non-abstract class.", exception);
            }
        }
    }
}
