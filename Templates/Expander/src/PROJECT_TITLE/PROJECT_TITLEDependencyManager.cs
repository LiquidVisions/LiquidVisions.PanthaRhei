using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;
using System.Diagnostics.CodeAnalysis;

namespace LiquidVisions.PanthaRhei.Expanders.PROJECT_TITLE
{
    /// <summary>
    /// Represents an implementation of <seealso cref="IExpanderDependencyManager"/> that allows dependency registration as part of a <seealso cref="PROJECT_TITLEExpander"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PROJECT_TITLEDependencyManager : AbstractExpanderDependencyManager<PROJECT_TITLEExpander>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PROJECT_TITLEDependencyManager"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="Expander"/></param>
        /// <param name="dependencyManager"><seealso cref="IDependencyFactory"/></param>
        public PROJECT_TITLEDependencyManager(Expander expander, IDependencyManager dependencyManager)
            : base(expander, dependencyManager)
        {
        }

        public override void Register()
        {
            base.Register();
        }
    }
}
