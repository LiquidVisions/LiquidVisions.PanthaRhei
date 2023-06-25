using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Parameters;

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
        /// <param name="dependencyManager"><seealso cref="IDependencyFactory"/></param>
        public CleanArchitectureDependencyManager(Expander expander, IDependencyManager dependencyManager)
            : base(expander, dependencyManager)
        {
        }

        public override void Register()
        {
            DependencyManager.AddTransient(typeof(IProjectTemplate), typeof(DotNetTemplate));
            DependencyManager.AddTransient(typeof(IElementTemplateParameters), typeof(RequestModelTemplateParameters));

            base.Register();
        }
    }
}
