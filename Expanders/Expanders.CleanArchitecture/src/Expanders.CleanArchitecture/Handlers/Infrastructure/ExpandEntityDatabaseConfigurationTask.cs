﻿using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Infrastructure
{
    /// <summary>
    /// Generates Fluid database configuration based on <seealso cref="Entity">Entities</seealso> into the Infrastructure library class.
    /// </summary>
    public class ExpandEntityDatabaseConfigurationTask : IExpanderTask<CleanArchitectureExpander>
    {
        private readonly ITemplate templateService;
        private readonly GenerationOptions options;
        private readonly CleanArchitectureExpander expander;
        private readonly App app;
        private readonly Component infrastructureComponent;
        private readonly Component domainComponent;
        private readonly string fullPathToTemplate;
        private readonly string targetFolderPath;
        private readonly IDirectory directory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandEntityDatabaseConfigurationTask"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandEntityDatabaseConfigurationTask(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            templateService = dependencyFactory.Resolve<ITemplate>();
            options = dependencyFactory.Resolve<GenerationOptions>();
            app = dependencyFactory.Resolve<App>();
            directory = dependencyFactory.Resolve<IDirectory>();

            infrastructureComponent = Expander.GetComponentByName(Resources.EntityFramework);
            domainComponent = Expander.GetComponentByName(Resources.Domain);

            fullPathToTemplate = Expander.Model.GetPathToTemplate(options, Resources.EntityDatabaseConfigurationTemplate);
            string componentOutputPath = expander.GetComponentOutputFolder(infrastructureComponent);
            targetFolderPath = Path.Combine(componentOutputPath, Resources.InfrastructureConfigurationFolder);
        }

        /// <inheritdoc/>
        public int Order => 8;

        /// <inheritdoc/>
        public string Name => nameof(ExpandEntityDatabaseConfigurationTask);

        /// <inheritdoc/>
        public CleanArchitectureExpander Expander => expander;

        /// <inheritdoc/>
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
