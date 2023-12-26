using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Initializers;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.PostProcessors;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Preprocessors;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Rejuvenators;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders
{
    /// <summary>
    /// Represents an abstract implementation of <seealso cref="IExpanderDependencyManager"/> that allows dependency registration as part of a <seealso cref="IExpander"/>.
    /// </summary>
    /// <typeparam name="TExpander"><seealso cref="IExpander"/></typeparam>
    public abstract class AbstractExpanderDependencyManager<TExpander> : IExpanderDependencyManager
        where TExpander : class, IExpander
    {
        private readonly IAssemblyManager _assemblyManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractExpanderDependencyManager{TExpander}"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="Model"/></param>
        /// <param name="dependencyManager"><seealso cref="IDependencyFactory"/></param>
        protected AbstractExpanderDependencyManager(Expander expander, IDependencyManager dependencyManager)
        {
            DependencyManager = dependencyManager;
            IDependencyFactory dependencyFactory = DependencyManager.Build();

            Model = expander;
            Logger = dependencyFactory.Resolve<ILogger>();
            _assemblyManager = dependencyFactory.Resolve<IAssemblyManager>();
        }

        /// <summary>
        /// Gets the <seealso cref="IDependencyManager"/>.
        /// </summary>
        [ExcludeFromCodeCoverage]
        protected IDependencyManager DependencyManager { get; }

        /// <summary>
        /// Gets the <seealso cref="Model"/>.
        /// </summary>
        [ExcludeFromCodeCoverage]
        protected Expander Model { get; }

        /// <summary>
        /// Gets the <seealso cref="ILogger"/>.
        /// </summary>
        [ExcludeFromCodeCoverage]
        protected ILogger Logger { get; }

        /// <inheritdoc/>
        public virtual void Register()
        {
            Assembly assembly = _assemblyManager.GetAssembly(GetType());

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
            DependencyManager.AddTransient(typeof(IRejuvenator<TExpander>), typeof(RegionRejuvenator<TExpander>));
            Logger.Debug($"Registered rejuvenator {typeof(IRejuvenator<TExpander>)} to match {nameof(RegionRejuvenator<TExpander>)} in the dependency container.");

            RegisterFromAssembly(assembly, typeof(IRejuvenator<TExpander>));
        }

        /// <summary>
        /// Register all types of a certain type that is part of the <seealso cref="Assembly"/>.
        /// </summary>
        /// <param name="assembly">The <seealso cref="Assembly"/> that contain the <seealso cref="Type">types</seealso> that should be registered.</param>
        /// <param name="serviceType">The type that should be registered.</param>
        public virtual void RegisterFromAssembly(Assembly assembly, Type serviceType)
        {
            ArgumentNullException.ThrowIfNull(assembly);

            IEnumerable<Type> list = assembly
                .GetExportedTypes()
                .Where(x => x.GetInterfaces().Contains(serviceType));

            foreach (Type type in list)
            {
                DependencyManager.AddTransient(serviceType, type);
                Logger.Debug($"Registered {serviceType} to match {type.Name} in the dependency container.");
            }
        }

        /// <summary>
        /// Registers all <seealso cref="IHarvester{TExpander}">Harvesters</seealso>.
        /// </summary>
        /// <param name="assembly">The <seealso cref="Assembly"/> that contain the <seealso cref="IHarvester{TExpander}">types</seealso> that should be registered.</param>
        public virtual void RegisterHarvesters(Assembly assembly)
        {
            DependencyManager.AddTransient(typeof(IHarvester<TExpander>), typeof(RegionHarvester<TExpander>));
            Logger.Debug($"Registered harvester {typeof(IHarvester<TExpander>)} to match {nameof(RegionHarvester<TExpander>)} in the dependency container.");

            RegisterFromAssembly(assembly, typeof(IHarvester<TExpander>));
        }

        /// <summary>
        /// Registers all <seealso cref="PostProcessor{TExpander}">PostProcessor</seealso>.
        /// </summary>
        /// <param name="assembly">The <seealso cref="Assembly"/> that contain the <seealso cref="PostProcessor{TExpander}">types</seealso> that should be registered.</param>
        public virtual void RegisterPostProcessors(Assembly assembly)
        {
            Logger.Debug($"Registering {nameof(UnInstallDotNetTemplate<TExpander>)} as a {nameof(IPostProcessor<TExpander>)}");
            DependencyManager.AddTransient(typeof(IPostProcessor<TExpander>), typeof(UnInstallDotNetTemplate<TExpander>));

            RegisterFromAssembly(assembly, typeof(IPostProcessor<TExpander>));
        }

        /// <summary>
        /// Registers all <seealso cref="PreProcessor{TExpander}">PreProcessors</seealso>.
        /// </summary>
        /// <param name="assembly">The <seealso cref="Assembly"/> that contain the <seealso cref="PreProcessor{TExpander}">types</seealso> that should be registered.</param>
        public virtual void RegisterPreProcessors(Assembly assembly)
        {
            Logger.Debug($"Registering {nameof(InstallDotNetTemplate<TExpander>)} as a {nameof(IPreProcessor<TExpander>)}");
            DependencyManager.AddTransient(typeof(IPreProcessor<TExpander>), typeof(InstallDotNetTemplate<TExpander>));

            RegisterFromAssembly(assembly, typeof(IPreProcessor<TExpander>));
        }

        /// <summary>
        /// Register all <seealso cref="IExpanderTask{TExpander}"/> that are loaded in the <paramref name="assembly"/>.
        /// </summary>
        /// <param name="assembly"><seealso cref="Assembly"/></param>
        public virtual void RegisterHandlers(Assembly assembly)
        {
            ArgumentNullException.ThrowIfNull(assembly);

            var listOfHandlers = assembly
                .GetExportedTypes()
                .Where(x => x.IsClass && !x.IsAbstract)
                .Where(x => x.GetInterfaces().Contains(typeof(IExpanderTask<TExpander>)))
                .ToList();

            if (listOfHandlers.Count == 0)
            {
                Logger.Warn($"Expander '{Model.Name}' does not have any {nameof(IExpanderTask<IExpander>)} implememntations.");
                return;
            }

            foreach (Type handlerType in listOfHandlers)
            {
                DependencyManager.AddTransient(typeof(IExpanderTask<TExpander>), handlerType);
                Logger.Trace($"Registered {typeof(IExpanderTask<TExpander>)} to match {handlerType} in the dependency container.");
            }
        }

        /// <summary>
        /// Register all <seealso cref="IExpander"/> that are loaded in the <paramref name="assembly"/>.
        /// </summary>
        /// <param name="assembly"><seealso cref="Assembly"/></param>
        public virtual void RegisterExpander(Assembly assembly)
        {
            ArgumentNullException.ThrowIfNull(assembly);

            try
            {
                Type expanderType = assembly.GetExportedTypes()
                    .Where(x => x.IsClass && !x.IsAbstract)
                    .Single(x => x.GetInterfaces().Contains(typeof(IExpander)));

                DependencyManager.AddTransient(typeof(IExpander), expanderType);
                DependencyManager.AddTransient(expanderType, expanderType);
                Logger.Trace($"Registered {expanderType} to match {typeof(IExpander)} in the dependency container.");
            }
            catch (InvalidOperationException exception)
            {
                throw new InitializationException($"Unable to load plugin '{Model.Name}'. No valid {nameof(IExpander)} derivatives found. The derivatives should be a non-abstract class.", exception);
            }
        }
    }
}
