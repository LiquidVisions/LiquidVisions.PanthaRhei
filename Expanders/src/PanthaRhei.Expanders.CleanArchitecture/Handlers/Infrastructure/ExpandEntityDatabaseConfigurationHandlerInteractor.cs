using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Domain.Interactors.Templates;
using LiquidVisions.PanthaRhei.Domain.IO;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Infrastructure
{
    /// <summary>
    /// Generates Fluid database configuration based on <seealso cref="Entity">Entities</seealso> into the Infrastructure library class.
    /// </summary>
    public class ExpandEntityDatabaseConfigurationHandlerInteractor : IExpanderHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly ITemplateInteractor templateService;
        private readonly GenerationOptions options;
        private readonly CleanArchitectureExpander expander;
        private readonly App app;
        private readonly Component infrastructureComponent;
        private readonly Component domainComponent;
        private readonly string fullPathToTemplate;
        private readonly string targetFolderPath;
        private readonly IDirectory directory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandEntityDatabaseConfigurationHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandEntityDatabaseConfigurationHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            templateService = dependencyFactory.Get<ITemplateInteractor>();
            options = dependencyFactory.Get<GenerationOptions>();
            app = dependencyFactory.Get<App>();
            directory = dependencyFactory.Get<IDirectory>();

            infrastructureComponent = Expander.GetComponentByName(Resources.EntityFramework);
            domainComponent = Expander.GetComponentByName(Resources.Domain);

            fullPathToTemplate = Expander.Model.GetPathToTemplate(options, Resources.EntityDatabaseConfigurationTemplate);
            string componentOutputPath = expander.GetComponentOutputFolder(infrastructureComponent);
            targetFolderPath = Path.Combine(componentOutputPath, Resources.InfrastructureConfigurationFolder);
        }

        public int Order => 8;

        public string Name => nameof(ExpandEntityDatabaseConfigurationHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool Enabled => options.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public void Execute()
        {
            directory.Create(targetFolderPath);

            foreach (Entity entity in app.Entities)
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

                var modelTemplate = new
                {
                    Entity = entity,
                    NameSpace = infrastructureComponent.GetComponentNamespace(app),
                    EntityNameSpace = domainComponent.GetComponentNamespace(app, Resources.DomainEntityFolder),
                    Indexes = indexes,
                    Keys = keys,
                };

                string savePath = Path.Combine(expander.GetComponentOutputFolder(infrastructureComponent), Resources.InfrastructureConfigurationFolder, $"{entity.Name}Configuration.cs");
                templateService.RenderAndSave(fullPathToTemplate, modelTemplate, savePath);
            }
        }
    }
}
