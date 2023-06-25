using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Domain.Interactors.Templates;
using LiquidVisions.PanthaRhei.Domain.IO;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Application
{
    /// <summary>
    /// a <seealso cref="IExpanderHandlerInteractor{CleanArchitectureExpander}"/> that adds the request models to the output project.
    /// </summary>
    public class ExpandRequestModelsHandlerInteractor : IExpanderHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly CleanArchitectureExpander expander;
        private readonly GenerationOptions options;
        private readonly ITemplateInteractor templateService;
        private readonly App app;
        private readonly IDirectory directory;
        private readonly string[] actions;
        private readonly Component component;
        private readonly string fullPathToComponentOutput;
        private readonly string destinationFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandRequestModelsHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandRequestModelsHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            options = dependencyFactory.Get<GenerationOptions>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
            app = dependencyFactory.Get<App>();
            directory = dependencyFactory.Get<IDirectory>();

            actions = Resources.DefaultRequestActions.Split(',', System.StringSplitOptions.TrimEntries);
            component = expander.GetComponentByName(Resources.Application);
            fullPathToComponentOutput = expander.GetComponentOutputFolder(component);
            destinationFolder = Path.Combine(fullPathToComponentOutput, Resources.RequestModelsFolder);
        }

        public int Order => 18;

        public string Name => nameof(ExpandRequestModelsHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool Enabled => options.CanExecuteDefaultAndExtend();

        public static string ToFileName(string action, Entity entity) =>
            action switch
            {
                "Get" => $"Get{entity.Name.Pluralize()}RequestModel",
                "GetById" => $"Get{entity.Name}ByIdRequestModel",
                _ => $"{action}{entity.Name}RequestModel"
            };

        public void Execute()
        {
            foreach (Entity entity in app.Entities)
            {
                string endpointFolder = Path.Combine(destinationFolder, entity.Name.Pluralize());
                directory.Create(endpointFolder);

                foreach (string action in actions)
                {
                    string fullPathToTemplate = Expander.Model.GetPathToTemplate(options, Resources.RequestModelTemplate);
                    string fullPathToFile = Path.Combine(endpointFolder, $"{ToFileName(action, entity)}.cs");
                    object templateModel = new
                    {
                        Action = action,
                        NS = Expander.Model.Name,
                        NameSpace = $"{component.GetComponentNamespace(app, Resources.RequestModelsFolder)}.{entity.Name.Pluralize()}",
                        Entity = entity,
                    };

                    templateService.RenderAndSave(fullPathToTemplate, templateModel, fullPathToFile);
                }
            }
        }
    }
}
