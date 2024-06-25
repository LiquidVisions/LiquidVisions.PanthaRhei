using System;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.PostProcessors;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Preprocessors;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Rejuvenators;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders
{
    /// <summary>
    /// An abstract implementation of the <see cref="IExpander"/>.
    /// </summary>
    /// <typeparam name="TExpander"><seealso cref="IExpander"/></typeparam>
    public abstract class AbstractExpander<TExpander> : IExpander
        where TExpander : class, IExpander
    {
        private readonly ILogger logger;
        private readonly IDependencyFactory dependencyFactory;
        private readonly IApplication application;
        private int order = int.MinValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractExpander{TExpander}"/> class.
        /// </summary>
        protected AbstractExpander()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractExpander{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        protected AbstractExpander(IDependencyFactory dependencyFactory)
        {
            ArgumentNullException.ThrowIfNull(dependencyFactory);

            this.dependencyFactory = dependencyFactory;

            application = dependencyFactory.Resolve<IApplication>();

            logger = dependencyFactory.Resolve<ILogger>();

            App = dependencyFactory.Resolve<App>();

            Model = App.Expanders
                .Single(x => x.Name == Name);

            order = Model.Order;
        }

        private static string GetName()
        {
            string[] fullName = typeof(TExpander).Namespace.Split('.');
            if(fullName.Length < 2)
            {
                return fullName[0];
            }

            return string.Join(".", fullName[^2], fullName[^1]);
        }

        /// <summary>
        /// Gets the <seealso cref="IApplication"/> instance.
        /// </summary>
        public IApplication Application => application;

        /// <inheritdoc/>
        public virtual string Name => GetName();
        /// <summary>
        /// Gets the <seealso cref="ILogger"/>.
        /// </summary>
        public ILogger Logger => logger;

        /// <summary>
        /// Gets the Model of the <seealso cref="AbstractExpander{TExpander}" /> as a <seealso cref="Expand"/>.
        /// </summary>
        public virtual Expander Model { get; }

        /// <summary>
        /// Gets the <seealso cref="App"/>.
        /// </summary>
        public virtual App App { get; }

        /// <summary>
        /// Gets the order of the <seealso cref="AbstractExpander{TExpander}"/>.
        /// </summary>
        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        /// <summary>
        /// Gets the <seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IExpanderTask{TExpander}"/>.
        /// </summary>e
        /// <returns><seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IExpanderTask{TExpander}"/>.</returns>
        public virtual IEnumerable<IExpanderTask<TExpander>> GetTasks()
        {
            IEnumerable<IExpanderTask<TExpander>> handlers = dependencyFactory.ResolveAll<IExpanderTask<TExpander>>();

            return handlers;
        }

        /// <summary>
        /// Gets the <seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IHarvester{TExpander}"/>.
        /// </summary>e
        /// <returns><seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IHarvester{TExpander}"/>.</returns>
        public virtual IEnumerable<IHarvester<TExpander>> GetHarvesters()
        {
            IEnumerable<IHarvester<TExpander>> harvesters = dependencyFactory.ResolveAll<IHarvester<TExpander>>();

            return harvesters;
        }

        /// <summary>
        /// Gets the <seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IPostProcessor{TExpander}"/>.
        /// </summary>e
        /// <returns><seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IPostProcessor{TExpander}"/>.</returns>
        public virtual IEnumerable<IPostProcessor<TExpander>> GetPostProcessor()
        {
            IEnumerable<IPostProcessor<TExpander>> postProcessors = dependencyFactory.ResolveAll<IPostProcessor<TExpander>>();

            return postProcessors;
        }

        /// <summary>
        /// Gets the <seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IPreProcessor{TExpander}"/>.
        /// </summary>e
        /// <returns><seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IPreProcessor{TExpander}"/>.</returns>
        public virtual IEnumerable<IPreProcessor<TExpander>> GetPreProcessor()
        {
            IEnumerable<IPreProcessor<TExpander>> preProcessors = dependencyFactory.ResolveAll<IPreProcessor<TExpander>>();

            return preProcessors;
        }

        /// <summary>
        /// Gets the <seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IRejuvenator{TExpander}"/>.
        /// </summary>e
        /// <returns><seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IRejuvenator{TExpander}"/>.</returns>
        public virtual IEnumerable<IRejuvenator<TExpander>> GetRejuvenators()
        {
            IEnumerable<IRejuvenator<TExpander>> rejuvenators = dependencyFactory.ResolveAll<IRejuvenator<TExpander>>();

            return rejuvenators;
        }

        /// <inheritdoc/>
        public virtual void Expand()
        {
            Logger.Trace($"Expanding expander {Name}");

            foreach (IExpanderTask<TExpander> task in GetTasks()
                .Where(x => x.Enabled)
                .OrderBy(x => x.Order))
            {
                task.Execute();
            }
        }

        /// <inheritdoc/>
        public void Harvest()
        {
            Logger.Trace($"Harvesting expander {Name} for expander {typeof(TExpander).Name}");

            IEnumerable<IHarvester<TExpander>> selectedHarvestHandlers = GetHarvesters();
            foreach (IHarvester<TExpander> task in selectedHarvestHandlers.Where(x => x.Enabled))
            {
                task.Execute();
            }
        }

        /// <inheritdoc/>
        public void Rejuvenate()
        {
            Logger.Trace($"Rejuvenating expander {Name}");

            IEnumerable<IRejuvenator<TExpander>> selectedRejuvenateHandlers = GetRejuvenators();
            foreach (IRejuvenator<TExpander> task in selectedRejuvenateHandlers.Where(x => x.Enabled))
            {
                task.Execute();
            }
        }

        /// <inheritdoc/>
        public void PostProcess()
        {
            Logger.Trace($"PostProcessing expander {Name}");

            IEnumerable<IPostProcessor<TExpander>> selectedPostProcessHandlers = GetPostProcessor();
            foreach (IPostProcessor<TExpander> task in selectedPostProcessHandlers.Where(x => x.Enabled))
            {
                task.Execute();
            }
        }

        /// <inheritdoc/>
        public void PreProcess()
        {
            Logger.Trace($"PreProcessing expander {Name}");

            IEnumerable<IPreProcessor<TExpander>> selectedPreProcessors = GetPreProcessor();
            foreach (IPreProcessor<TExpander> task in selectedPreProcessors.Where(x => x.Enabled))
            {
                task.Execute();
            }
        }

        /// <inheritdoc/>
        public abstract void Clean();
    }
}
