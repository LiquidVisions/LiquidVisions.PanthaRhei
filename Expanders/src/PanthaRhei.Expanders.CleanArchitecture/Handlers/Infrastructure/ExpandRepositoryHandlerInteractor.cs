using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Infrastructure
{
    /// <summary>
    /// Generates the DbContext class into the Infrastructure library.
    /// </summary>
    public class ExpandRepositoryHandlerInteractor : IExpanderTask<CleanArchitectureExpander>
    {
        private readonly ITemplateInteractor templateService;
        private readonly GenerationOptions options;
        private readonly App app;
        private readonly Component component;
        private readonly Component applicationComponent;
        private readonly string fullPathToRepositoryFolder;
        private readonly CleanArchitectureExpander expander;
        private readonly IDirectory directory;
        private readonly string fullPathToTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandRepositoryHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandRepositoryHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            templateService = dependencyFactory.Get<ITemplateInteractor>();
            options = dependencyFactory.Get<GenerationOptions>();
            app = dependencyFactory.Get<App>();

            component = expander.GetComponentByName(Resources.EntityFramework);
            applicationComponent = expander.GetComponentByName(Resources.Application);

            fullPathToRepositoryFolder = System.IO.Path.Combine(expander.GetComponentOutputFolder(component), Resources.RepositoryFolder);
            directory = dependencyFactory.Get<IDirectory>();

            fullPathToTemplate = Expander.Model.GetPathToTemplate(options, Resources.RepositoryTemplate);
        }

        public int Order => 11;

        public string Name => nameof(ExpandRepositoryHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool Enabled => options.CanExecuteDefaultAndExtend();

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

                string filePath = Path.Combine(expander.GetComponentOutputFolder(component), Resources.RepositoryFolder, $"{entity.Name}Repository.cs");
                templateService.RenderAndSave(fullPathToTemplate, templateModel, filePath);
            }
        }
    }
}