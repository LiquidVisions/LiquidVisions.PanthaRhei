using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.PostProcessors;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Preprocessors;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Rejuvenator;

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
            this.dependencyFactory = dependencyFactory;

            logger = this.dependencyFactory.Get<ILogger>();
            App = dependencyFactory.Get<App>();
            Model = App.Expanders
                .Single(x => x.Name == Name);
        }

        /// <inheritdoc/>
        public virtual string Name => typeof(TExpander).Name
            .Replace("Expander", string.Empty);

        /// <summary>
        /// Gets the <seealso cref="ILogger"/>.
        /// </summary>
        public ILogger Logger => logger;

        /// <summary>
        /// Gets the Model of the <seealso cref="AbstractExpander{TExpander}" /> as a <seealso cref="Expand"/>.
        /// </summary>
        public virtual Expander Model { get; }

        public virtual App App { get; }

        public virtual int Order => Model == null ? GetOrder() : Model.Order;

        /// <summary>
        /// Gets the <seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IExpanderTask{TExpander}"/>.
        /// </summary>e
        /// <returns><seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IExpanderTask{TExpander}"/>.</returns>
        public virtual IEnumerable<IExpanderTask<TExpander>> GetHandlers()
        {
            IEnumerable<IExpanderTask<TExpander>> handlers = dependencyFactory.GetAll<IExpanderTask<TExpander>>();

            return handlers;
        }

        /// <summary>
        /// Gets the <seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IHarvester{TExpander}"/>.
        /// </summary>e
        /// <returns><seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IHarvester{TExpander}"/>.</returns>
        public virtual IEnumerable<IHarvester<TExpander>> GetHarvesters()
        {
            IEnumerable<IHarvester<TExpander>> harvesters = dependencyFactory.GetAll<IHarvester<TExpander>>();

            return harvesters;
        }

        /// <summary>
        /// Gets the <seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IPostProcessor{TExpander}"/>.
        /// </summary>e
        /// <returns><seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IPostProcessor{TExpander}"/>.</returns>
        public virtual IEnumerable<IPostProcessor<TExpander>> GetPostProcessor()
        {
            IEnumerable<IPostProcessor<TExpander>> postProcessors = dependencyFactory.GetAll<IPostProcessor<TExpander>>();

            return postProcessors;
        }

        /// <summary>
        /// Gets the <seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IPreProcessor{TExpander}"/>.
        /// </summary>e
        /// <returns><seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IPreProcessor{TExpander}"/>.</returns>
        public virtual IEnumerable<IPreProcessor<TExpander>> GetPreProcessor()
        {
            IEnumerable<IPreProcessor<TExpander>> preProcessors = dependencyFactory.GetAll<IPreProcessor<TExpander>>();

            return preProcessors;
        }

        /// <summary>
        /// Gets the <seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IRejuvenator{TExpander}"/>.
        /// </summary>e
        /// <returns><seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IRejuvenator{TExpander}"/>.</returns>
        public virtual IEnumerable<IRejuvenator<TExpander>> GetRejuvenators()
        {
            IEnumerable<IRejuvenator<TExpander>> rejuvenators = dependencyFactory.GetAll<IRejuvenator<TExpander>>();

            return rejuvenators;
        }

        /// <inheritdoc/>
        public virtual void Expand()
        {
            Logger.Trace($"Expanding expander {Name}");

            foreach (IExpanderTask<TExpander> handler in GetHandlers()
                .Where(x => x.Enabled)
                .OrderBy(x => x.Order))
            {
                handler.Execute();
            }
        }

        /// <inheritdoc/>
        public void Harvest()
        {
            Logger.Trace($"Harvesting expander {Name} for expander {typeof(TExpander).Name}");

            IEnumerable<IHarvester<TExpander>> selectedHarvestHandlers = GetHarvesters();
            foreach (var handler in selectedHarvestHandlers.Where(x => x.Enabled))
            {
                handler.Execute();
            }
        }

        /// <inheritdoc/>
        public void Rejuvenate()
        {
            Logger.Trace($"Rejuvenating expander {Name}");

            var selectedRejuvenateHandlers = GetRejuvenators();
            foreach (var handler in selectedRejuvenateHandlers.Where(x => x.Enabled))
            {
                handler.Execute();
            }
        }

        /// <inheritdoc/>
        public void PostProcess()
        {
            Logger.Trace($"PostProcessing expander {Name}");

            var selectedPostProcessHandlers = GetPostProcessor();
            foreach (var handler in selectedPostProcessHandlers.Where(x => x.Enabled))
            {
                handler.Execute();
            }
        }

        /// <inheritdoc/>
        public void PreProcess()
        {
            Logger.Trace($"PreProcessing expander {Name}");

            var selectedPrePocessors = GetPreProcessor();
            foreach (var handler in selectedPrePocessors.Where(x => x.Enabled))
            {
                handler.Execute();
            }
        }

        /// <inheritdoc/>
        public abstract void Clean();

        /// <summary>
        /// Gets the order of the expander.
        /// </summary>
        /// <returns>The order of the expander.</returns>
        protected abstract int GetOrder();
    }
}
