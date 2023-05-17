using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Infrastructure
{
    /// <summary>
    /// Generates the DbContext class into the Infrastructure library.
    /// </summary>
    public class ExpandRepositoryHandlerInteractor : IExpanderHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;
        private readonly GenerationOptions options;
        private readonly App app;
        private readonly Generator.Domain.Entities.Component component;
        private readonly Generator.Domain.Entities.Component applicationComponent;
        private readonly string fullPathToRepositoryFolder;
        private readonly CleanArchitectureExpander expander;
        private readonly IDirectory directory;
        private readonly string fullPathToTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandRepositoryHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ExpandRepositoryHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            this.expander = expander;

            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
            options = dependencyFactory.Get<GenerationOptions>();
            app = dependencyFactory.Get<App>();

            component = expander.Model.GetComponentByName(Resources.EntityFramework);
            applicationComponent = expander.Model.GetComponentByName(Resources.Application);

            fullPathToRepositoryFolder = System.IO.Path.Combine(projectAgent.GetComponentOutputFolder(component), Resources.RepositoryFolder);
            directory = dependencyFactory.Get<IDirectory>();

            fullPathToTemplate = Expander.Model.GetPathToTemplate(options, Resources.RepositoryTemplate);
        }

        public int Order => 11;

        public string Name => nameof(ExpandRepositoryHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => options.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public void Execute()
        {
            directory.Create(fullPathToRepositoryFolder);

            foreach (Entity entity in app.Entities)
            {
                var templateModel = new
                {
                    entity,
                    component,
                    applicationComponent,
                };

                string filePath = Path.Combine(projectAgent.GetComponentOutputFolder(component), Resources.RepositoryFolder, $"{entity.Name}Repository.cs");
                templateService.RenderAndSave(fullPathToTemplate, templateModel, filePath);
            }
        }
    }
}