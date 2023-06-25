using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Domain.Interactors.Templates;
using LiquidVisions.PanthaRhei.Domain.IO;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api
{
    /// <summary>
    /// Add presenters classes to the output.
    /// </summary>
    public class ExpandPresentersHandlerInteractor : IExpanderTask<CleanArchitectureExpander>
    {
        private readonly CleanArchitectureExpander expander;
        private readonly GenerationOptions options;
        private readonly App app;
        private readonly IDirectory directory;
        private readonly ITemplateInteractor templateService;
        private readonly string[] requestActions;
        private readonly Component component;
        private readonly Component applicationComponent;
        private readonly string destinationFolder;
        private readonly string fullPathToTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandPresentersHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandPresentersHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            options = dependencyFactory.Get<GenerationOptions>();
            app = dependencyFactory.Get<App>();
            directory = dependencyFactory.Get<IDirectory>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();

            requestActions = Resources.DefaultRequestActions.Split(',', System.StringSplitOptions.TrimEntries);

            component = Expander.GetComponentByName(Resources.Api);
            applicationComponent = Expander.GetComponentByName(Resources.Application);

            string projectOutputFolder = expander.GetComponentOutputFolder(component);
            destinationFolder = Path.Combine(projectOutputFolder, Resources.PresentersFolder);
            fullPathToTemplate = Expander.Model.GetPathToTemplate(options, Resources.PresenterTemplate);
        }

        public int Order => 14;

        public string Name => nameof(ExpandPresentersHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool Enabled => options.CanExecuteDefaultAndExtend();

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
