using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Domain
{
    /// <summary>
    /// A Custom expander implementing <seealso cref="AbstractExpander{DomainExpander}" />.
    /// </summary>
    public class DomainExpander : AbstractExpander<DomainExpander>
    {
        private readonly GenerationOptions options;

        public DomainExpander()
        {
        }

        public DomainExpander(IDependencyFactory dependencyFactory)
            : base(dependencyFactory)
        {
            options = dependencyFactory.Get<GenerationOptions>();
        }

        public override string Name => "CleanArchitecture.Domain";

        /// <inheritdoc/>
        public override void Expand()
        {
            base.Expand();
        }

        /// <inheritdoc/>
        public override void Clean()
        {
            // Do Nothing, yet!
        }

        /// <inheritdoc/>
        protected override int GetOrder() => Model.Order;

        internal virtual string GetComponentOutputFolder(Component component)
        {
            return Path.Combine(options.OutputFolder, App.FullName, "src", $"{component.Name}");
        }

        internal virtual IEnumerable<string> GetComponentPaths(params string[] componentNames)
        {
            foreach (string componentName in componentNames)
            {
                Component components = GetComponentByName(componentName);
                yield return this.GetComponentOutputFolder(components);
            }
        }

        internal virtual Component GetComponentByName(string name) => Model
                .Components
                .Single(x => x.Name == name);

        internal virtual string GetComponentProjectFile(Component component)
        {
            return Path.Combine(GetComponentOutputFolder(component), $"{component.Name}.csproj");
        }
    }
}
