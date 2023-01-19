﻿using LiquidVisions.PanthaRhei.Generator.Domain;
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
        private readonly CleanArchitectureExpander expander;
        private readonly Parameters parameters;
        private readonly App app;
        private readonly Component component;
        private readonly Component clientComponent;
        private readonly Component applicationComponent;
        private readonly string fullPathToTemplate;
        private readonly string fullPathToViewModelsFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddViewModelMappers"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddViewModelMappers(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            this.expander = expander;

            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
            parameters = dependencyFactory.Get<Parameters>();
            app = dependencyFactory.Get<App>();

            component = Expander.Model.GetComponentByName(Resources.Api);
            clientComponent = Expander.Model.GetComponentByName(Resources.Client);
            applicationComponent = Expander.Model.GetComponentByName(Resources.Application);

            string fullPathToApiComponent = projectAgent.GetComponentOutputFolder(component);
            fullPathToViewModelsFolder = System.IO.Path.Combine(fullPathToApiComponent, Resources.ViewModelMapperFolder);
            fullPathToTemplate = Expander.Model.GetTemplateFolder(parameters, Resources.ViewModelMapperTemplate);

            dependencyFactory
                .Get<IDirectory>()
                .Create(fullPathToViewModelsFolder);
        }

        public int Order => 15;

        public string Name => nameof(AddViewModelMappers);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => parameters.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public void Execute()
        {
            foreach (var entity in app.Entities)
            {
                object templateModel = new
                {
                    Entity = entity,
                    component,
                    clientComponent,
                    applicationComponent,
                };

                string fullPath = System.IO.Path.Combine(fullPathToViewModelsFolder, $"{entity.Name}ModelMapper.cs");
                templateService.RenderAndSave(fullPathToTemplate, templateModel, fullPath);
            }
        }
    }
}
