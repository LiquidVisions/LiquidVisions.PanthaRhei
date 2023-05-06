using System.IO;
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
    public class ExpandRequestModelsHandlerInteractor : IExpanderHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly CleanArchitectureExpander expander;
        private readonly ExpandRequestModel expandRequestModel;
        private readonly IProjectAgentInteractor projectAgent;
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
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ExpandRequestModelsHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            this.expander = expander;

            expandRequestModel = dependencyFactory.Get<ExpandRequestModel>();
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
            app = dependencyFactory.Get<App>();
            directory = dependencyFactory.Get<IDirectory>();

            actions = Resources.DefaultRequestActions.Split(',', System.StringSplitOptions.TrimEntries);
            component = expander.Model.GetComponentByName(Resources.Application);
            fullPathToComponentOutput = projectAgent.GetComponentOutputFolder(component);
            destinationFolder = Path.Combine(fullPathToComponentOutput, Resources.RequestModelsFolder);
        }

        public int Order => 18;

        public string Name => nameof(ExpandRequestModelsHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => expandRequestModel.CanExecuteDefaultAndExtend();

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
                    string fullPathToTemplate = Expander.Model.GetPathToTemplate(expandRequestModel, Resources.RequestModelTemplate);
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
