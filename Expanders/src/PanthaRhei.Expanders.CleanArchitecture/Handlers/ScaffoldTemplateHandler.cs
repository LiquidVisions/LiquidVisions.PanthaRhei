using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Handlers;

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

        public override int Order => 1;

        /// <inheritdoc/>
        public override void Execute()
        {
            Scaffold(Resources.TemplateShortName, App.Name, App.FullName);

            //Expander.Model.Components
            //    ?.ForEach(component => component.Packages
            //    ?.ForEach(package =>
            //    {
            //        string project = Path.Combine(Parameters.OutputFolder, App.FullName, "src", $"{App.Name}.{component.Name}", $"{App.Name}.{component.Name}.csproj");
            //        ApplyNugetPackages(project, package.Name, package.Version);
            //    }));
        }
    }
}
