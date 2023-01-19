using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api
{
    /// <summary>
    /// Add presenters classes to the output.
    /// </summary>
    public class AddPresenters : IHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly CleanArchitectureExpander expander;
        private readonly Parameters parameters;
        private readonly App app;
        private readonly IDirectory directory;
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;
        private readonly string[] requestActions;
        private readonly Component component;
        private readonly Component applicationComponent;
        private readonly Component clientComponent;
        private readonly string destinationFolder;
        private readonly string fullPathToTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPresenters"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddPresenters(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            this.expander = expander;

            parameters = dependencyFactory.Get<Parameters>();
            app = dependencyFactory.Get<App>();
            directory = dependencyFactory.Get<IDirectory>();
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();

            requestActions = Resources.DefaultRequestActions.Split(',', System.StringSplitOptions.TrimEntries);

            component = Expander.Model.GetComponentByName(Resources.Api);
            applicationComponent = Expander.Model.GetComponentByName(Resources.Application);
            clientComponent = Expander.Model.GetComponentByName(Resources.Client);

            string projectOutputFolder = projectAgent.GetComponentOutputFolder(component);
            destinationFolder = Path.Combine(projectOutputFolder, Resources.PresentersFolder);
            fullPathToTemplate = Expander.Model.GetTemplateFolder(parameters, Resources.PresenterTemplate);
        }

        public int Order => 14;

        public string Name => nameof(AddPresenters);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => parameters.CanExecuteDefaultAndExtend();

        public void Execute()
        {
            foreach (Entity endpoint in app.Entities)
            {
                string endpointFolder = Path.Combine(destinationFolder, endpoint.Name.Pluralize());
                directory.Create(endpointFolder);

                foreach (string action in requestActions)
                {
                    string fullPath = Path.Combine(endpointFolder, $"{endpoint.ToFileName(action, "Presenter")}.cs");
                    object templateModel = new
                    {
                        clientComponent,
                        applicationComponent,
                        component,
                        action,
                        entity = endpoint,
                    };

                    templateService.RenderAndSave(fullPathToTemplate, templateModel, fullPath);
                }
            }
        }
    }
}
