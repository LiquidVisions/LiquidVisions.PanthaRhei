using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Application
{
    /// <summary>
    /// a <seealso cref="RequestActionsTemplateHandlerService"/> that adds the boundaries to the output project.
    /// </summary>
    public class AddBoundaries : IHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly CleanArchitectureExpander expander;
        private readonly Parameters parameters;
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;
        private readonly App app;
        private readonly IDirectory directory;

        private readonly List<string> actions;
        private readonly Component component;
        private readonly Component clientComponent;
        private readonly string fullPathToComponentOutput;
        private readonly string fullPathToTemplate;
        private readonly string destinationFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddBoundaries"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddBoundaries(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            this.expander = expander;

            parameters = dependencyFactory.Get<Parameters>();
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
            app = dependencyFactory.Get<App>();
            directory = dependencyFactory.Get<IDirectory>();

            actions = Resources.DefaultRequestActions.Split(',', System.StringSplitOptions.TrimEntries).ToList();
            component = expander.Model.GetComponentByName(Resources.Application);
            clientComponent = expander.Model.GetComponentByName(Resources.Client);
            fullPathToComponentOutput = projectAgent.GetComponentOutputFolder(component);
            fullPathToTemplate = Expander.Model.GetTemplateFolder(parameters, Resources.BoundaryTemplate);
            destinationFolder = Path.Combine(fullPathToComponentOutput, Resources.ApplicationBoundariesFolder);
        }

        public int Order => 3;

        public string Name => nameof(AddBoundaries);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => parameters.CanExecuteDefaultAndExtend();

        public void Execute()
        {
            foreach (Entity entity in app.Entities)
            {
                string endpointFolder = Path.Combine(destinationFolder, entity.Name.Pluralize());
                directory.Create(endpointFolder);

                foreach (string action in actions)
                {
                    string fullPathToFile = Path.Combine(endpointFolder, $"{entity.ToFileName(action, "Boundary")}.cs");
                    object templateModel = new
                    {
                        clientComponent,
                        component,
                        ActionType = action,
                        entity = entity,
                    };

                    templateService.RenderAndSave(fullPathToTemplate, templateModel, fullPathToFile);
                }
            }
        }
    }
}
