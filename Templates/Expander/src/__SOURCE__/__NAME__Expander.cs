using System;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;

namespace __PREFIX__.__SOURCE__
{
    /// <summary>
    /// A Custom expander implementing <seealso cref="AbstractExpander{__NAME__Expander}" />.
    /// </summary>
    public class __NAME__Expander : AbstractExpander<__NAME__Expander>
    {
        private readonly GenerationOptions options;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="__NAME__Expander"/> class.
        /// </summary>
        public __NAME__Expander()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="__NAME__Expander"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public __NAME__Expander(IDependencyFactory dependencyFactory)
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
