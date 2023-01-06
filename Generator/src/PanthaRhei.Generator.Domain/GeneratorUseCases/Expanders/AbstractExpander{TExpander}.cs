using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Harvesters;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.PostProcessors;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Preprocessors;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Rejuvenator;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Expanders
{
    /// <summary>
    /// An abstract implementation of the <see cref="IExpander"/>.
    /// </summary>
    /// <typeparam name="TExpander"><seealso cref="IExpander"/></typeparam>
    public abstract class AbstractExpander<TExpander> : IExpander
        where TExpander : class, IExpander
    {
        private readonly ILogger logger;
        private readonly IDependencyResolver dependencyResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractExpander{TExpander}"/> class.
        /// </summary>
        protected AbstractExpander()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractExpander{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyResolver"><seealso cref="IDependencyResolver"/></param>
        protected AbstractExpander(IDependencyResolver dependencyResolver)
        {
            this.dependencyResolver = dependencyResolver;

            logger = this.dependencyResolver.Get<ILogger>();
            Model = dependencyResolver.Get<App>()
                .Expanders
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

        public virtual int Order => Model == null ? GetOrder() : Model.Order;

        protected abstract int GetOrder();

        /// <summary>
        /// Gets the <seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="AbstractHandler{TExpander}"/>.
        /// </summary>e
        /// <returns><seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="AbstractHandler{TExpander}"/>.</returns>
        public virtual IEnumerable<IHandler<TExpander>> GetHandlers()
        {
            IEnumerable<IHandler<TExpander>> handlers = dependencyResolver.GetAll<IHandler<TExpander>>();

            return handlers;
        }

        /// <summary>
        /// Gets the <seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IHarvester{TExpander}"/>.
        /// </summary>e
        /// <returns><seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IHarvester{TExpander}"/>.</returns>
        public virtual IEnumerable<IHarvester<TExpander>> GetHarvesters()
        {
            IEnumerable<IHarvester<TExpander>> harvesters = dependencyResolver.GetAll<IHarvester<TExpander>>();

            return harvesters;
        }

        /// <summary>
        /// Gets the <seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IPostProcessor{TExpander}"/>.
        /// </summary>e
        /// <returns><seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IPostProcessor{TExpander}"/>.</returns>
        public virtual IEnumerable<IPostProcessor<TExpander>> GetPostProcessor()
        {
            IEnumerable<IPostProcessor<TExpander>> postProcessors = dependencyResolver.GetAll<IPostProcessor<TExpander>>();

            return postProcessors;
        }

        /// <summary>
        /// Gets the <seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IPreProcessor{TExpander}"/>.
        /// </summary>e
        /// <returns><seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IPreProcessor{TExpander}"/>.</returns>
        public virtual IEnumerable<IPreProcessor<TExpander>> GetPreProcessor()
        {
            IEnumerable<IPreProcessor<TExpander>> preProcessors = dependencyResolver.GetAll<IPreProcessor<TExpander>>();

            return preProcessors;
        }

        /// <summary>
        /// Gets the <seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IRejuvenator{TExpander}"/>.
        /// </summary>e
        /// <returns><seealso cref="IEnumerable{IHandler}">collection</seealso> of <seealso cref="IRejuvenator{TExpander}"/>.</returns>
        public virtual IEnumerable<IRejuvenator<TExpander>> GetRejuvenators()
        {
            IEnumerable<IRejuvenator<TExpander>> rejuvenators = dependencyResolver.GetAll<IRejuvenator<TExpander>>();

            return rejuvenators;
        }

        /// <inheritdoc/>
        public virtual void Expand()
        {
            Logger.Trace($"Expanding expander {Name}");

            foreach (IHandler<TExpander> handler in GetHandlers()
                .Where(x => x.CanExecute)
                .OrderBy(x => x.Model.Order))
            {
                handler.Execute();
            }
        }

        /// <inheritdoc/>
        public void Harvest()
        {
            Logger.Trace($"Harvesting expander {Name} for expander {typeof(TExpander).Name}");

            IEnumerable<IHarvester<TExpander>> selectedHarvestHandlers = GetHarvesters();
            foreach (var handler in selectedHarvestHandlers.Where(x => x.CanExecute))
            {
                handler.Execute();
            }
        }

        /// <inheritdoc/>
        public void Rejuvenate()
        {
            Logger.Trace($"Rejuvenating expander {Name}");

            var selectedRejuvenateHandlers = GetRejuvenators();
            foreach (var handler in selectedRejuvenateHandlers.Where(x => x.CanExecute))
            {
                handler.Execute();
            }
        }

        /// <inheritdoc/>
        public void PostProcess()
        {
            Logger.Trace($"PostProcessing expander {Name}");

            var selectedPostProcessHandlers = GetPostProcessor();
            foreach (var handler in selectedPostProcessHandlers.Where(x => x.CanExecute))
            {
                handler.Execute();
            }
        }

        /// <inheritdoc/>
        public void PreProcess()
        {
            Logger.Trace($"PreProcessing expander {Name}");

            var selectedPrePocessors = GetPreProcessor();
            foreach (var handler in selectedPrePocessors.Where(x => x.CanExecute))
            {
                handler.Execute();
            }
        }
    }
}
