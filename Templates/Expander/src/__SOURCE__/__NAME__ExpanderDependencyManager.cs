using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;
using System.Diagnostics.CodeAnalysis;

namespace __PREFIX__.__SOURCE__
{
    /// <summary>
    /// Represents an implementation of <seealso cref="IExpanderDependencyManager"/> that allows dependency registration as part of a <seealso cref="__SHORTNAME__Expander"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class __SHORTNAME__ExpanderDependencyManager : AbstractExpanderDependencyManager<__SHORTNAME__Expander>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="__SHORTNAME__Expander"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="Expander"/></param>
        /// <param name="dependencyManager"><seealso cref="IDependencyFactory"/></param>
        public __SHORTNAME__ExpanderDependencyManager(Expander expander, IDependencyManager dependencyManager)
            : base(expander, dependencyManager)
        {
        }

        public override void Register()
        {
            base.Register();
        }
    }
}

