using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Rejuvenator
{
    /// <summary>
    /// An abstract implementation of the <see cref="IRejuvenator{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpander"/>.</typeparam>
    public abstract class Rejuvenator<TExpander> : IRejuvenator<TExpander>
        where TExpander : class, IExpander
    {
        private readonly TExpander expander;
        private readonly App app;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rejuvenator{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyResolver"><seealso cref="IDependencyResolver"/></param>
        protected Rejuvenator(IDependencyResolver dependencyResolver)
        {
            app = dependencyResolver.Get<App>();
            expander = dependencyResolver.Get<TExpander>();
        }

        /// <inheritdoc/>
        public abstract bool CanExecute { get; }

        /// <inheritdoc/>
        public App App => app;

        /// <inheritdoc/>
        public TExpander Expander => expander;

        /// <summary>
        /// Gets the extension of the harvest file.
        /// </summary>
        protected abstract string Extension { get; }

        /// <inheritdoc/>
        public abstract void Execute();
    }
}
