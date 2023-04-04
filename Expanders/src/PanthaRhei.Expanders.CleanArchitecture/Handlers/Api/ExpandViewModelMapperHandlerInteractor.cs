using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api
{
    /// <summary>
    /// Generates the mappers for the viewmodels.
    /// </summary>
    public class ExpandViewModelMapperHandlerInteractor : IExpanderHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;
        private readonly CleanArchitectureExpander expander;
        private readonly Parameters parameters;
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
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ExpandViewModelMapperHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            this.expander = expander;

            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
            parameters = dependencyFactory.Get<Parameters>();
            app = dependencyFactory.Get<App>();
            directory = dependencyFactory.Get<IDirectory>();

            component = Expander.Model.GetComponentByName(Resources.Api);
            applicationComponent = Expander.Model.GetComponentByName(Resources.Application);

            string fullPathToApiComponent = projectAgent.GetComponentOutputFolder(component);
            fullPathToViewModelsFolder = System.IO.Path.Combine(fullPathToApiComponent, Resources.ViewModelMapperFolder);
            fullPathToTemplate = Expander.Model.GetPathToTemplate(parameters, Resources.ViewModelMapperTemplate);
        }

        public int Order => 15;

        public string Name => nameof(ExpandViewModelMapperHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => parameters.CanExecuteDefaultAndExtend();

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
