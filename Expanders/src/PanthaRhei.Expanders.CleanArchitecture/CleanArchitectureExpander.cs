using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;

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
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public CleanArchitectureExpander(IDependencyFactoryInteractor dependencyFactory)
            : base(dependencyFactory)
        {
        }

        protected override int GetOrder() => 1;
    }
}