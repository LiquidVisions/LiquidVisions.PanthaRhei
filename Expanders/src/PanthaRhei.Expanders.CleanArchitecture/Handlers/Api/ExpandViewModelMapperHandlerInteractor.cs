using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api
{
    /// <summary>
    /// Generates the mappers for the viewmodels.
    /// </summary>
    public class ExpandViewModelMapperHandlerInteractor : IExpanderTask<CleanArchitectureExpander>
    {
        private readonly ITemplateInteractor templateService;
        private readonly CleanArchitectureExpander expander;
        private readonly GenerationOptions options;
        private readonly App app;
        private readonly Component component;
        private readonly Component applicationComponent;
        private readonly string fullPathToTemplate;
        private readonly IDirectory directory;
        private readonly string fullPathToViewModelsFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandViewModelMapperHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandViewModelMapperHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            templateService = dependencyFactory.Get<ITemplateInteractor>();
            options = dependencyFactory.Get<GenerationOptions>();
            app = dependencyFactory.Get<App>();
            directory = dependencyFactory.Get<IDirectory>();

            component = Expander.GetComponentByName(Resources.Api);
            applicationComponent = Expander.GetComponentByName(Resources.Application);

            string fullPathToApiComponent = expander.GetComponentOutputFolder(component);
            fullPathToViewModelsFolder = System.IO.Path.Combine(fullPathToApiComponent, Resources.ViewModelMapperFolder);
            fullPathToTemplate = Expander.Model.GetPathToTemplate(options, Resources.ViewModelMapperTemplate);
        }

        public int Order => 15;

        public string Name => nameof(ExpandViewModelMapperHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool Enabled => options.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public void Execute()
        {
            directory.Create(fullPathToViewModelsFolder);

            foreach (var entity in app.Entities)
            {
                object templateModel = new
                {
                    Entity = entity,
                    component,
                    applicationComponent,
                };

                string fullPath = System.IO.Path.Combine(fullPathToViewModelsFolder, $"{entity.Name}ViewModelMapper.cs");
                templateService.RenderAndSave(fullPathToTemplate, templateModel, fullPath);
            }
        }
    }
}
