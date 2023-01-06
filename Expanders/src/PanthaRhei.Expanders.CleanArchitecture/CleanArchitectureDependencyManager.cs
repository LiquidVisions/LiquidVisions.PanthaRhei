using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture
{
    /// <summary>
    /// Represents an implementation of <seealso cref="IExpanderDependencyManager"/> that allows dependency registration as part of a <seealso cref="CleanArchitectureExpander"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CleanArchitectureDependencyManager : AbstractExpanderDependencyManager<CleanArchitectureExpander>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CleanArchitectureDependencyManager"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="Expander"/></param>
        /// <param name="dependencyManager"><seealso cref="IDependencyResolver"/></param>
        public CleanArchitectureDependencyManager(Expander expander, IDependencyManager dependencyManager)
            : base(expander, dependencyManager)
        {
        }
    }
}
