using System;
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
        /// <param name="dependencyResolver"><seealso cref="IDependencyResolver"/></param>
        public ScaffoldTemplateHandler(IDependencyResolver dependencyResolver)
            : base(dependencyResolver)
        {
        }

        /// <inheritdoc/>
        public override bool CanExecute => Parameters.Clean;

        /// <inheritdoc/>
        public override void Execute()
        {
            Scaffold(Resources.TemplateShortName, App.Name, Expander.Model.Name);

            foreach (Component component in Expander.Model.Components)
            {
                if (component.Packages != null)
                {
                    foreach (Package package in component.Packages)
                    {
                        PackageReference(string.Empty, package.Name, package.Version);
                    }
                }
            }
        }
    }
}
