using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Application
{
    /// <summary>
    /// Generates the ViewModels for the <seealso cref="Entity">Entities</seealso>.
    /// </summary>
    public class ExpandViewModelsTask : IExpanderTask<CleanArchitectureExpander>
    {
        private readonly ITemplate templateService;
        private readonly App app;
        private readonly GenerationOptions options;
        private readonly Component component;
        private readonly string viewModelsFolder;
        private readonly IDirectory directory;
        private readonly CleanArchitectureExpander expander;
        private readonly string fullPathToTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandViewModelsTask"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandViewModelsTask(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            templateService = dependencyFactory.Get<ITemplate>();
            app = dependencyFactory.Get<App>();
            options = dependencyFactory.Get<GenerationOptions>();
            directory = dependencyFactory.Get<IDirectory>();

            component = expander.GetComponentByName(Resources.Api);
            string fullPathToComponentOutput = expander.GetComponentOutputFolder(component);
            viewModelsFolder = Path.Combine(fullPathToComponentOutput, Resources.ViewModelsFolder);
            fullPathToTemplate = Expander.Model.GetPathToTemplate(options, Resources.ViewModelTemplate);
        }

        /// <inheritdoc/>
        public int Order => 17;

        /// <inheritdoc/>
        public string Name => nameof(ExpandViewModelsTask);

        /// <inheritdoc/>
        public CleanArchitectureExpander Expander => expander;

        /// <inheritdoc/>
        public bool Enabled => options.CanExecuteDefaultAndExtend();

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
