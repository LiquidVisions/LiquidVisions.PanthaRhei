using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Rejuvenator
{
    /// <summary>
    /// An abstract implementation of the <see cref="IRejuvenatorInteractor{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpander"/>.</typeparam>
    public abstract class RejuvenatorInteractor<TExpander> : IRejuvenatorInteractor<TExpander>
        where TExpander : class, IExpander
    {
        private readonly TExpander expander;
        private readonly App app;

        /// <summary>
        /// Initializes a new instance of the <see cref="RejuvenatorInteractor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        protected RejuvenatorInteractor(IDependencyFactory dependencyFactory)
        {
            app = dependencyFactory.Get<App>();
            expander = dependencyFactory.Get<TExpander>();
        }

        /// <inheritdoc/>
        public abstract bool Enabled { get; }

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
