using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Infrastructure
{
    /// <summary>
    /// Generates the DbContext class into the Infrastructure library.
    /// </summary>
    public class ExpandDatabaseContext : AbstractHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandDatabaseContext"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ExpandDatabaseContext(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
            : base(expander, dependencyFactory)
        {
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
        }

        public override int Order => 9;

        /// <inheritdoc/>
        public override void Execute()
        {
            Component domain = Expander.Model.GetComponentByName(Resources.Domain);
            Component infrastructure = Expander.Model.GetComponentByName(Resources.EntityFramework);

            var parameters = new
            {
                App.Entities,
                ConnectionString = App.ConnectionStrings.Single(x => x.Name == "DefaultConnectionString").Definition,
                NameSpace = infrastructure.GetComponentNamespace(App),
                NameSpaceEntities = domain.GetComponentNamespace(App, Resources.DomainEntityFolder),
            };

            string fullPathToTemplate = Expander.Model.GetTemplateFolder(Parameters, Resources.DbContextTemplate);
            string result = templateService.Render(fullPathToTemplate, parameters);

            string path = System.IO.Path.Combine(projectAgent.GetComponentOutputFolder(infrastructure), "Context.cs");
            File.WriteAllText(path, result);
        }
    }
}