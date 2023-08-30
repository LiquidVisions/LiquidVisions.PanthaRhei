using System;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Rejuvenator
{
    /// <summary>
    /// An abstract implementation of the <see cref="IRejuvenator{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpander"/>.</typeparam>
    public abstract class Rejuvenator<TExpander> : IRejuvenator<TExpander>
        where TExpander : class, IExpander
    {
        private readonly TExpander _expander;
        private readonly App _app;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rejuvenator{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        protected Rejuvenator(IDependencyFactory dependencyFactory)
        {
            ArgumentNullException.ThrowIfNull(dependencyFactory);

            _app = dependencyFactory.Resolve<App>();
            _expander = dependencyFactory.Resolve<TExpander>();
        }

        /// <inheritdoc/>
        public abstract bool Enabled { get; }

        /// <inheritdoc/>
        public App App => _app;

        /// <inheritdoc/>
        public TExpander Expander => _expander;

        /// <summary>
        /// Gets the extension of the harvest file.
        /// </summary>
        protected abstract string Extension { get; }

        /// <inheritdoc/>
        public abstract void Execute();
    }
}
