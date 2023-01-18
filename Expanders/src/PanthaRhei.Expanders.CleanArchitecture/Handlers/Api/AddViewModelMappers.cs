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
    public class AddViewModelMappers : IHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;
        private readonly IDirectory directory;
        private readonly CleanArchitectureExpander expander;
        private readonly Parameters parameters;
        private readonly App app;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddViewModelMappers"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddViewModelMappers(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
            directory = dependencyFactory.Get<IDirectory>();
            parameters = dependencyFactory.Get<Parameters>();
            app = dependencyFactory.Get<App>();

            this.expander = expander;
        }

        public int Order => 15;

        public string Name => nameof(AddViewModelMappers);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => parameters.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public void Execute()
        {
            Component component = Expander.Model.GetComponentByName(Resources.Api);
            Component clientComponent = Expander.Model.GetComponentByName(Resources.Client);
            Component applicationComponent = Expander.Model.GetComponentByName(Resources.Application);

            string path = projectAgent.GetComponentOutputFolder(component);
            string viewModelsFolder = System.IO.Path.Combine(path, Resources.ViewModelMapperFolder);
            directory.Create(viewModelsFolder);

            string fullPathToTemplate = Expander.Model.GetTemplateFolder(parameters, Resources.ViewModelMapperTemplate);

            foreach (var entity in app.Entities)
            {
                object templateModel = new
                {
                    Entity = entity,
                    component,
                    clientComponent,
                    applicationComponent,
                };

                string fullPath = System.IO.Path.Combine(viewModelsFolder, $"{entity.Name}ModelMapper.cs");
                templateService.RenderAndSave(fullPathToTemplate, templateModel, fullPath);
            }
        }
    }
}
