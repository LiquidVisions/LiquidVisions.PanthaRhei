using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Initializers;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.PostProcessors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Preprocessors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Rejuvenator;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders
{
    /// <summary>
    /// Represents an abstract implementation of <seealso cref="IExpanderDependencyManagerInteractor"/> that allows dependency registration as part of a <seealso cref="IExpanderInteractor"/>.
    /// </summary>
    /// <typeparam name="TExpander"><seealso cref="IExpanderInteractor"/></typeparam>
    public abstract class AbstractExpanderDependencyManagerInteractor<TExpander> : IExpanderDependencyManagerInteractor
        where TExpander : class, IExpanderInteractor
    {
        private readonly Expander expander;
        private readonly IDependencyManagerInteractor dependencyManager;
        private readonly ILogger logger;
        private readonly IAssemblyManagerInteractor assemblyManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractExpanderDependencyManagerInteractor{TExpander}"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="Model"/></param>
        /// <param name="dependencyManager"><seealso cref="IDependencyFactoryInteractor"/></param>
        protected AbstractExpanderDependencyManagerInteractor(Expander expander, IDependencyManagerInteractor dependencyManager)
        {
            this.dependencyManager = dependencyManager;
            IDependencyFactoryInteractor dependencyFactory = this.dependencyManager.Build();

            this.expander = expander;
            logger = dependencyFactory.Get<ILogger>();
            assemblyManager = dependencyFactory.Get<IAssemblyManagerInteractor>();
        }

        /// <summary>
        /// Gets the <seealso cref="IDependencyManagerInteractor"/>.
        /// </summary>
        [ExcludeFromCodeCoverage]
        protected IDependencyManagerInteractor DependencyManager => dependencyManager;

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
        /// Registers all <seealso cref="RejuvenatorInteractor{TExpander}">Rejuvenators</seealso>.
        /// </summary>
        /// <param name="assembly">The <seealso cref="Assembly"/> that contain the <seealso cref="RejuvenatorInteractor{TExpander}">types</seealso> that should be registered.</param>
        public virtual void RegisterRejuvenators(Assembly assembly)
        {
            dependencyManager.AddTransient(typeof(IRejuvenatorInteractor<TExpander>), typeof(RegionRejuvenatorInteractor<TExpander>));
            logger.Debug($"Registered rejuvenator {typeof(IRejuvenatorInteractor<TExpander>)} to match {nameof(RegionRejuvenatorInteractor<TExpander>)} in the dependency container.");

            RegisterFromAssembly(assembly, typeof(IRejuvenatorInteractor<TExpander>));
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
        /// Registers all <seealso cref="HarvesterInteractor{TExpander}">Harvesters</seealso>.
        /// </summary>
        /// <param name="assembly">The <seealso cref="Assembly"/> that contain the <seealso cref="HarvesterInteractor{TExpander}">types</seealso> that should be registered.</param>
        public virtual void RegisterHarvesters(Assembly assembly)
        {
            dependencyManager.AddTransient(typeof(IHarvesterInteractor<TExpander>), typeof(RegionHarvesterInteractor<TExpander>));
            logger.Debug($"Registered harvester {typeof(IHarvesterInteractor<TExpander>)} to match {nameof(RegionHarvesterInteractor<TExpander>)} in the dependency container.");

            RegisterFromAssembly(assembly, typeof(IHarvesterInteractor<TExpander>));
        }

        /// <summary>
        /// Registers all <seealso cref="PostProcessorInteractor{TExpander}">PostProcessor</seealso>.
        /// </summary>
        /// <param name="assembly">The <seealso cref="Assembly"/> that contain the <seealso cref="PostProcessorInteractor{TExpander}">types</seealso> that should be registered.</param>
        public virtual void RegisterPostProcessors(Assembly assembly)
        {
            logger.Debug($"Registering {nameof(UnInstallDotNetTemplateInteractor<TExpander>)} as a {nameof(IPostProcessorInteractor<TExpander>)}");
            dependencyManager.AddTransient(typeof(IPostProcessorInteractor<TExpander>), typeof(UnInstallDotNetTemplateInteractor<TExpander>));

            RegisterFromAssembly(assembly, typeof(IPostProcessorInteractor<TExpander>));
        }

        /// <summary>
        /// Registers all <seealso cref="PreProcessorInteractor{TExpander}">PreProcessors</seealso>.
        /// </summary>
        /// <param name="assembly">The <seealso cref="Assembly"/> that contain the <seealso cref="PreProcessorInteractor{TExpander}">types</seealso> that should be registered.</param>
        public virtual void RegisterPreProcessors(Assembly assembly)
        {
            logger.Debug($"Registering {nameof(InstallDotNetTemplateInteractor<TExpander>)} as a {nameof(IPreProcessorInteractor<TExpander>)}");
            dependencyManager.AddTransient(typeof(IPreProcessorInteractor<TExpander>), typeof(InstallDotNetTemplateInteractor<TExpander>));

            RegisterFromAssembly(assembly, typeof(IPreProcessorInteractor<TExpander>));
        }

        /// <summary>
        /// Register all <seealso cref="IHandlerInteractor{TExpander}"/> that are loaded in the <paramref name="assembly"/>.
        /// </summary>
        /// <param name="assembly"><seealso cref="Assembly"/></param>
        public virtual void RegisterHandlers(Assembly assembly)
        {
            var listOfHandlers = assembly
                .GetExportedTypes()
                .Where(x => x.IsClass && !x.IsAbstract)
                .Where(x => x.GetInterfaces().Contains(typeof(IHandlerInteractor<TExpander>)))
                .ToList();

            if (!listOfHandlers.Any())
            {
                logger.Warn($"Expander '{expander.Name}' does not have any {nameof(IHandlerInteractor<IExpanderInteractor>)} implememntations.");
                return;
            }

            foreach (Type handlerType in listOfHandlers)
            {
                dependencyManager.AddTransient(typeof(IHandlerInteractor<TExpander>), handlerType);
                logger.Trace($"Registered {typeof(IHandlerInteractor<TExpander>)} to match {handlerType} in the dependency container.");
            }
        }

        /// <summary>
        /// Register all <seealso cref="IExpanderInteractor"/> that are loaded in the <paramref name="assembly"/>.
        /// </summary>
        /// <param name="assembly"><seealso cref="Assembly"/></param>
        public virtual void RegisterExpander(Assembly assembly)
        {
            try
            {
                Type expanderType = assembly.GetExportedTypes()
                    .Where(x => x.IsClass && !x.IsAbstract)
                    .Single(x => x.GetInterfaces().Contains(typeof(IExpanderInteractor)));

                dependencyManager.AddTransient(typeof(IExpanderInteractor), expanderType);
                dependencyManager.AddTransient(expanderType, expanderType);
                logger.Trace($"Registered {expanderType} to match {typeof(IExpanderInteractor)} in the dependency container.");
            }
            catch (InvalidOperationException exception)
            {
                throw new InitializationException($"Unable to load plugin '{expander.Name}'. No valid {nameof(IExpanderInteractor)} derivatives found. The derivatives should be a non-abstract class.", exception);
            }
        }
    }
}
