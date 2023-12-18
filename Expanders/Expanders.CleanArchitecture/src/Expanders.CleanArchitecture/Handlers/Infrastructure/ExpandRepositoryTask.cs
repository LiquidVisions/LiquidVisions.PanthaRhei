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
    public class ExpandRepositoryTask : IExpanderTask<CleanArchitectureExpander>
    {
        private readonly ITemplate templateService;
        private readonly GenerationOptions options;
        private readonly App app;
        private readonly Component component;
        private readonly Component applicationComponent;
        private readonly string fullPathToRepositoryFolder;
        private readonly CleanArchitectureExpander expander;
        private readonly IDirectory directory;
        private readonly string fullPathToTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandRepositoryTask"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandRepositoryTask(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            templateService = dependencyFactory.Resolve<ITemplate>();
            options = dependencyFactory.Resolve<GenerationOptions>();
            app = dependencyFactory.Resolve<App>();

            component = expander.GetComponentByName(Resources.EntityFramework);
            applicationComponent = expander.GetComponentByName(Resources.Application);

            fullPathToRepositoryFolder = System.IO.Path.Combine(expander.GetComponentOutputFolder(component), Resources.RepositoryFolder);
            directory = dependencyFactory.Resolve<IDirectory>();

            fullPathToTemplate = Expander.Model.GetPathToTemplate(options, Resources.RepositoryTemplate);
        }

        /// <inheritdoc/>
        public int Order => 11;

        /// <inheritdoc/>
        public string Name => nameof(ExpandRepositoryTask);

        /// <inheritdoc/>
        public CleanArchitectureExpander Expander => expander;

        /// <inheritdoc/>
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