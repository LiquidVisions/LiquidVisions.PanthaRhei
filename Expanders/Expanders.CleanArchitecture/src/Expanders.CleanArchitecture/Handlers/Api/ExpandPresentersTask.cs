﻿using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api
{
    /// <summary>
    /// Add presenters classes to the output.
    /// </summary>
    public class ExpandPresentersTask : IExpanderTask<CleanArchitectureExpander>
    {
        private readonly CleanArchitectureExpander expander;
        private readonly GenerationOptions options;
        private readonly App app;
        private readonly IDirectory directory;
        private readonly ITemplate templateService;
        private readonly string[] requestActions;
        private readonly Component component;
        private readonly Component applicationComponent;
        private readonly string destinationFolder;
        private readonly string fullPathToTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandPresentersTask"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandPresentersTask(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            options = dependencyFactory.Resolve<GenerationOptions>();
            app = dependencyFactory.Resolve <App>();
            directory = dependencyFactory.Resolve<IDirectory>();
            templateService = dependencyFactory.Resolve<ITemplate>();

            requestActions = Resources.DefaultRequestActions.Split(',', System.StringSplitOptions.TrimEntries);

            component = Expander.GetComponentByName(Resources.Api);
            applicationComponent = Expander.GetComponentByName(Resources.Application);

            string projectOutputFolder = expander.GetComponentOutputFolder(component);
            destinationFolder = Path.Combine(projectOutputFolder, Resources.PresentersFolder);
            fullPathToTemplate = Expander.Model.GetPathToTemplate(options, Resources.PresenterTemplate);
        }

        /// <inheritdoc/>
        public int Order => 14;
        
        /// <inheritdoc/>
        public string Name => nameof(ExpandPresentersTask);

        /// <inheritdoc/>
        public CleanArchitectureExpander Expander => expander;

        /// <inheritdoc/>
        public bool Enabled => options.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public void Execute()
        {
            foreach (Entity entity in app.Entities)
            {
                string endpointFolder = Path.Combine(destinationFolder, entity.Name.Pluralize());
                directory.Create(endpointFolder);

                foreach (string action in requestActions)
                {
                    string fullPath = Path.Combine(endpointFolder, $"{entity.ToFileName(action, "Presenter")}.cs");
                    object templateModel = new
                    {
                        applicationComponent,
                        component,
                        action,
                        entity,
                    };

                    templateService.RenderAndSave(fullPathToTemplate, templateModel, fullPath);
                }
            }
        }
    }
}
