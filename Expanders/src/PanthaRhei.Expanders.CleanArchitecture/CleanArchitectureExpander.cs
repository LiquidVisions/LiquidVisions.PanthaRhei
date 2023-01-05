using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture
{
    /// <summary>
    /// The <seealso cref="CleanArchitectureExpander"/> expanders.
    /// </summary>
    public class CleanArchitectureExpander : AbstractExpander<CleanArchitectureExpander>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CleanArchitectureExpander"/> class.
        /// </summary>
        public CleanArchitectureExpander()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CleanArchitectureExpander"/> class.
        /// </summary>
        /// <param name="dependencyResolver"><seealso cref="IDependencyResolver"/></param>
        public CleanArchitectureExpander(IDependencyResolver dependencyResolver)
            : base(dependencyResolver)
        {
        }

        protected override int GetOrder() => 1;
    }
}