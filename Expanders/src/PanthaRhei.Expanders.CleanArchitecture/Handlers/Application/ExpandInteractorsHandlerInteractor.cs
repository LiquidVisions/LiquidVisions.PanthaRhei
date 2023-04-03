using System.Collections.Generic;
using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Application
{
    /// <summary>
    /// a <seealso cref="IExpanderHandlerInteractor{CleanArchitectureExpander}"/> that adds the request models to the output project.
    /// </summary>
    public class ExpandInteractorsHandlerInteractor : IExpanderHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly CleanArchitectureExpander expander;
        private readonly Parameters parameters;
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;
        private readonly App app;
        private readonly IDirectory directory;

        private readonly List<string> actions;
        private readonly Component component;
        private readonly string fullPathToComponentOutput;
        private readonly string destinationFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandInteractorsHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ExpandInteractorsHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            this.expander = expander;

            parameters = dependencyFactory.Get<Parameters>();
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
            app = dependencyFactory.Get<App>();
            directory = dependencyFactory.Get<IDirectory>();

            actions = Resources.DefaultRequestActions.Split(',', System.StringSplitOptions.TrimEntries).ToList();
            component = expander.Model.GetComponentByName(Resources.Application);
            fullPathToComponentOutput = projectAgent.GetComponentOutputFolder(component);
            destinationFolder = Path.Combine(fullPathToComponentOutput, Resources.InteractorFolder);
        }

        public int Order => 5;

        public string Name => nameof(ExpandInteractorsHandlerInteractor);

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
                    string fullPathToTemplate = Expander.Model.GetPathToTemplate(parameters, $"{action}{Resources.InteractorTemplate}");
                    string fullPathToFile = Path.Combine(endpointFolder, $"{entity.ToFileName(action, "Interactor")}.cs");
                    object templateModel = new
                    {
                        component,
                        Entity = entity,
                    };

                    templateService.RenderAndSave(fullPathToTemplate, templateModel, fullPathToFile);
                }
            }
        }
    }
}
