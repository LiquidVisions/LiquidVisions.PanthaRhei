﻿using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Application
{
    /// <summary>
    /// Generates the mappers for the application models.
    /// </summary>
    public class AddApplicationMappers : IHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly CleanArchitectureExpander expander;
        private readonly Parameters parameters;
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;
        private readonly App app;
        private readonly IDirectory directory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddApplicationMappers"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddApplicationMappers(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            this.expander = expander;

            parameters = dependencyFactory.Get<Parameters>();
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
            app = dependencyFactory.Get<App>();
            directory = dependencyFactory.Get<IDirectory>();
        }

        public int Order => 4;

        public string Name => nameof(AddApplicationMappers);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => parameters.CanExecuteDefaultAndExtend();

        public void Execute()
        {
            string[] actions = new string[] { "Create", "Update" };

            Component component = Expander.Model.GetComponentByName(Resources.Application);
            Component clientComponent = Expander.Model.GetComponentByName(Resources.Client);

            string fullPathToComponent = projectAgent.GetComponentOutputFolder(component);
            string fullPathToRootFolder = Path.Combine(fullPathToComponent, Resources.ApplicationMapperFolder);
            string fullPathToTemplate = Expander.Model.GetTemplateFolder(parameters, Resources.ApplicationMapperTemplate);

            foreach (Entity endpoint in app.Entities)
            {
                string fullpathToDestinationFolder = Path.Combine(fullPathToRootFolder, endpoint.Name.Pluralize());
                directory.Create(fullpathToDestinationFolder);

                foreach (string action in actions)
                {
                    string filePath = Path.Combine(fullpathToDestinationFolder, $"{action}{endpoint.Name}CommandTo{endpoint.Name}Mapper.cs");
                    object templateModel = new
                    {
                        clientComponent,
                        component,
                        Action = action,
                        Entity = endpoint,
                    };

                    templateService.RenderAndSave(fullPathToTemplate, templateModel, filePath);
                }
            }
        }
    }
}
