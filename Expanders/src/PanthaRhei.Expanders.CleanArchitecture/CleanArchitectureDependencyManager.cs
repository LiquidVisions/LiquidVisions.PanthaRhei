using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Parameters;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture
{
    /// <summary>
    /// Represents an implementation of <seealso cref="IExpanderDependencyManagerInteractor"/> that allows dependency registration as part of a <seealso cref="CleanArchitectureExpander"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CleanArchitectureDependencyManager : AbstractExpanderDependencyManagerInteractor<CleanArchitectureExpander>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CleanArchitectureDependencyManager"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="Expander"/></param>
        /// <param name="dependencyManager"><seealso cref="IDependencyFactoryInteractor"/></param>
        public CleanArchitectureDependencyManager(Expander expander, IDependencyManagerInteractor dependencyManager)
            : base(expander, dependencyManager)
        {
        }

        public override void Register()
        {
            DependencyManager.AddTransient(typeof(IProjectTemplateInteractor), typeof(DotNetTemplateInteractor));
            DependencyManager.AddTransient(typeof(IElementTemplateParameters), typeof(RequestModelTemplateParameters));

            base.Register();
        }
    }
}
