using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Infrastructure
{
    /// <summary>
    /// Generates Fluid database configuration based on <seealso cref="Entity">Entities</seealso> into the Infrastructure library class.
    /// </summary>
    public class ExpandEntityDatabaseConfigurationHandlerInteractor : AbstractHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandEntityDatabaseConfigurationHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ExpandEntityDatabaseConfigurationHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
            : base(expander, dependencyFactory)
        {
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
        }

        public override int Order => 8;

        /// <inheritdoc/>
        public override void Execute()
        {
            Component infrastructureComponent = Expander.Model.GetComponentByName(Resources.EntityFramework);
            Component domainComponent = Expander.Model.GetComponentByName(Resources.Domain);

            string targetFolderPath = Path.Combine(projectAgent.GetComponentOutputFolder(infrastructureComponent), Resources.InfrastructureConfigurationFolder);
            Directory.Create(targetFolderPath);

            foreach (Entity entity in App.Entities)
            {
                string[] indexes = entity.Fields
                    .Where(x => x.IsIndex)
                    .Select(x => x.Name)
                    .ToArray();

                string[] keys = entity.Fields
                    .OrderBy(x => x.Order)
                    .Where(x => x.IsKey)
                    .Select(x => x.Name)
                    .ToArray();

                var parameters = new
                {
                    Entity = entity,
                    NameSpace = infrastructureComponent.GetComponentNamespace(App),
                    EntityNameSpace = domainComponent.GetComponentNamespace(App, Resources.DomainEntityFolder),
                    Indexes = indexes,
                    Keys = keys,
                };

                string fullPathToTemplate = Expander.Model.GetTemplateFolder(Parameters, Resources.EntityDatabaseConfigurationTemplate);
                string result = templateService.Render(fullPathToTemplate, parameters);

                string path = System.IO.Path.Combine(projectAgent.GetComponentOutputFolder(infrastructureComponent), "Configuration.cs");
                File.WriteAllText(path, result);
            }
        }
    }
}
