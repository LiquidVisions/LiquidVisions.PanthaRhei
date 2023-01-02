using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Generators.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers
{
    /// <summary>
    /// Generates the solution and projects using the dotnet cli command liquidvisions-ca.
    /// </summary>
    public class ScaffoldTemplateHandler : AbstractScaffoldDotNetTemplateHandler<CleanArchitectureExpander>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScaffoldTemplateHandler "/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyResolver"><seealso cref="IDependencyResolver"/></param>
        public ScaffoldTemplateHandler(CleanArchitectureExpander expander, IDependencyResolver dependencyResolver)
            : base(expander, dependencyResolver)
        {
        }

        /// <inheritdoc/>
        public override bool CanExecute => Parameters.Clean;

        /// <inheritdoc/>
        public override void Execute()
        {
            Scaffold(Resources.TemplateShortName, App.Name, App.FullName);

            Model.Expander.Components
                ?.ForEach(component => component.Packages
                ?.ForEach(package =>
                {
                    string project = $"{App.FullName}.{component.Name}";
                    ApplyNugetPackages(project, package.Name, package.Version);
                }));
        }
    }
}
