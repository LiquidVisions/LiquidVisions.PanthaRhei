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
    /// Generates the mappers for the application models.
    /// </summary>
    public class ExpandApplicationMappersHandlerInteractor : IExpanderHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly CleanArchitectureExpander expander;
        private readonly GenerationOptions options;
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;
        private readonly App app;
        private readonly IDirectory directory;
        private readonly string[] actions;
        private readonly Component component;
        private readonly string fullPathToRootFolder;
        private readonly string fullPathToTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandApplicationMappersHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ExpandApplicationMappersHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            this.expander = expander;

            options = dependencyFactory.Get<GenerationOptions>();
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
            app = dependencyFactory.Get<App>();
            directory = dependencyFactory.Get<IDirectory>();

            actions = new string[] { "Create", "Update" };

            component = Expander.Model.GetComponentByName(Resources.Application);

            string fullPathToComponent = projectAgent.GetComponentOutputFolder(component);
            fullPathToRootFolder = Path.Combine(fullPathToComponent, Resources.ApplicationMapperFolder);
            fullPathToTemplate = Expander.Model.GetPathToTemplate(options, Resources.ApplicationMapperTemplate);
        }

        public int Order => 4;

        public string Name => nameof(ExpandApplicationMappersHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => options.CanExecuteDefaultAndExtend();

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
