using System;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Application
{
    /// <summary>
    /// A Custom expander implementing <seealso cref="AbstractExpander{ApplicationExpander}" />.
    /// </summary>
    public class ApplicationExpander : AbstractExpander<ApplicationExpander>
    {
        private readonly GenerationOptions options;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationExpander"/> class.
        /// </summary>
        public ApplicationExpander()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationExpander"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ApplicationExpander(IDependencyFactory dependencyFactory)
            : base(dependencyFactory)
        {
            ArgumentNullException.ThrowIfNull(dependencyFactory, nameof(dependencyFactory));

            options = dependencyFactory.Resolve<GenerationOptions>();
        }
        
        /// <inheritdoc/>
        public override string Name => "CleanArchitecture.Application";

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
