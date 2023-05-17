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
    /// Generates the ViewModels for the <seealso cref="Entity">Entities</seealso>.
    /// </summary>
    public class ExpandViewModelsHandlerInteractor : IExpanderHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;
        private readonly App app;
        private readonly GenerationOptions options;
        private readonly Component component;
        private readonly string viewModelsFolder;
        private readonly IDirectory directory;
        private readonly CleanArchitectureExpander expander;
        private readonly string fullPathToTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandViewModelsHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ExpandViewModelsHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            this.expander = expander;

            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
            app = dependencyFactory.Get<App>();
            options = dependencyFactory.Get<GenerationOptions>();
            directory = dependencyFactory.Get<IDirectory>();

            component = Expander.Model.GetComponentByName(Resources.Api);
            string fullPathToComponentOutput = projectAgent.GetComponentOutputFolder(component);
            viewModelsFolder = Path.Combine(fullPathToComponentOutput, Resources.ViewModelsFolder);
            fullPathToTemplate = Expander.Model.GetPathToTemplate(options, Resources.ViewModelTemplate);
        }

        public int Order => 17;

        public string Name => nameof(ExpandViewModelsHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => options.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public void Execute()
        {
            directory.Create(viewModelsFolder);

            foreach (Entity entity in app.Entities)
            {
                var templateModel = new
                {
                    component,
                    Entity = entity,
                };

                string path = Path.Combine(viewModelsFolder, $"{entity.Name}ViewModel.cs");
                templateService.RenderAndSave(fullPathToTemplate, templateModel, path);
            }
        }
    }
}
