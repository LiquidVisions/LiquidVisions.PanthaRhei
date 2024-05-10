using System;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;

namespace __PREFIX__.__SOURCE__
{
    /// <summary>
    /// A Custom expander implementing <seealso cref="AbstractExpander{__SHORTNAME__Expander}" />.
    /// </summary>
    public class __SHORTNAME__Expander : AbstractExpander<__SHORTNAME__Expander>
    {
        private readonly GenerationOptions options;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="__SHORTNAME__Expander"/> class.
        /// </summary>
        public __SHORTNAME__Expander()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="__SHORTNAME__Expander"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public __SHORTNAME__Expander(IDependencyFactory dependencyFactory)
            : base(dependencyFactory)
        {
            ArgumentNullException.ThrowIfNull(dependencyFactory, nameof(dependencyFactory));

            options = dependencyFactory.Resolve<GenerationOptions>();
        }
        
        /// <inheritdoc/>
        public override string Name => "__SOURCE__";

        /// <inheritdoc/>
        public override void Expand()
        {
            base.Expand();
        }

        /// <inheritdoc/>
        public override void Clean()
        {
            // Nothing, yet?
        }
        
        /// <inheritdoc/>
        protected override int GetOrder() 
            => Model.Order;
    }
}
