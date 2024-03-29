﻿using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Application
{
    /// <summary>
    /// Generates the mappers for the application models.
    /// </summary>
    public class ExpandApplicationMappersTask : IExpanderTask<CleanArchitectureExpander>
    {
        private readonly CleanArchitectureExpander expander;
        private readonly GenerationOptions options;
        private readonly ITemplate templateService;
        private readonly App app;
        private readonly IDirectory directory;
        private readonly string[] actions;
        private readonly Component component;
        private readonly string fullPathToRootFolder;
        private readonly string fullPathToTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandApplicationMappersTask"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandApplicationMappersTask(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            options = dependencyFactory.Resolve<GenerationOptions>();
            templateService = dependencyFactory.Resolve<ITemplate>();
            app = dependencyFactory.Resolve<App>();
            directory = dependencyFactory.Resolve<IDirectory>();

            actions = new string[] { "Create", "Update" };

            component = Expander.GetComponentByName(Resources.Application);

            string fullPathToComponent = expander.GetComponentOutputFolder(component);
            fullPathToRootFolder = Path.Combine(fullPathToComponent, Resources.ApplicationMapperFolder);
            fullPathToTemplate = Expander.Model.GetPathToTemplate(options, Resources.ApplicationMapperTemplate);
        }

        /// <inheritdoc/>
        public int Order => 4;

        /// <inheritdoc/>
        public string Name => nameof(ExpandApplicationMappersTask);

        /// <inheritdoc/>
        public CleanArchitectureExpander Expander => expander;

        /// <inheritdoc/>
        public bool Enabled => options.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public void Execute()
        {
            foreach (Entity entity in app.Entities)
            {
                string fullpathToDestinationFolder = Path.Combine(fullPathToRootFolder, entity.Name.Pluralize());
                directory.Create(fullpathToDestinationFolder);

                foreach (string action in actions)
                {
                    string filePath = Path.Combine(fullpathToDestinationFolder, $"{action}{entity.Name}RequestModelMapper.cs");
                    object templateModel = new
                    {
                        component,
                        Action = action,
                        Entity = entity,
                    };

                    templateService.RenderAndSave(fullPathToTemplate, templateModel, filePath);
                }
            }
        }
    }
}
