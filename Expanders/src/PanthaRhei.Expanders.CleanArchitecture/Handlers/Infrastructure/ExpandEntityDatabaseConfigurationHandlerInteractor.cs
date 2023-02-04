﻿using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Infrastructure
{
    /// <summary>
    /// Generates Fluid database configuration based on <seealso cref="Entity">Entities</seealso> into the Infrastructure library class.
    /// </summary>
    public class ExpandEntityDatabaseConfigurationHandlerInteractor : IExpanderHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;
        private readonly Parameters parameters;
        private readonly CleanArchitectureExpander expander;
        private readonly App app;
        private readonly Component infrastructureComponent;
        private readonly Component domainComponent;
        private readonly string fullPathToTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandEntityDatabaseConfigurationHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ExpandEntityDatabaseConfigurationHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            this.expander = expander;

            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();

            parameters = dependencyFactory.Get<Parameters>();
            app = dependencyFactory.Get<App>();

            infrastructureComponent = Expander.Model.GetComponentByName(Resources.EntityFramework);
            domainComponent = Expander.Model.GetComponentByName(Resources.Domain);

            fullPathToTemplate = Expander.Model.GetTemplateFolder(parameters, Resources.EntityDatabaseConfigurationTemplate);
            string componentOutputPath = projectAgent.GetComponentOutputFolder(infrastructureComponent);
            string targetFolderPath = Path.Combine(componentOutputPath, Resources.InfrastructureConfigurationFolder);
            dependencyFactory
                .Get<IDirectory>()
                .Create(targetFolderPath);
        }

        public int Order => 8;

        public string Name => nameof(ExpandEntityDatabaseConfigurationHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => parameters.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public void Execute()
        {
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

                string savePath = Path.Combine(projectAgent.GetComponentOutputFolder(infrastructureComponent), $"{entity.Name}Configuration.cs");
                templateService.RenderAndSave(fullPathToTemplate, modelTemplate, savePath);
            }
        }
    }
}