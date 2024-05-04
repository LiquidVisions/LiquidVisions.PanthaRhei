using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;
using System.Diagnostics.CodeAnalysis;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Application
{
    /// <summary>
    /// Represents an implementation of <seealso cref="IExpanderDependencyManager"/> that allows dependency registration as part of a <seealso cref="ApplicationExpander"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ApplicationExpanderDependencyManager : AbstractExpanderDependencyManager<ApplicationExpander>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationExpander"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="Expander"/></param>
        /// <param name="dependencyManager"><seealso cref="IDependencyFactory"/></param>
        public ApplicationExpanderDependencyManager(Expander expander, IDependencyManager dependencyManager)
            : base(expander, dependencyManager)
        {
        }

        public override void Register()
        {
            base.Register();
        }
    }
}

