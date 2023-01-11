using System.Net;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api
{
    /// <summary>
    /// Generates the mappers for the viewmodels.
    /// </summary>
    public class AddViewModelMappers : AbstractHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IWriterInteractor writer;
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddViewModelMappers"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddViewModelMappers(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
            : base(expander, dependencyFactory)
        {
            writer = dependencyFactory.Get<IWriterInteractor>();
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
        }

        public override int Order => 15;

        /// <inheritdoc/>
        public override void Execute()
        {
            Component component = Expander.Model.GetComponentByName(Resources.Api);
            Component clientComponent = Expander.Model.GetComponentByName(Resources.Client);
            Component applicationComponent = Expander.Model.GetComponentByName(Resources.Application);

            string path = projectAgent.GetComponentOutputFolder(component);
            string viewModelsFolder = System.IO.Path.Combine(path, Resources.ViewModelMapperFolder);
            Directory.Create(viewModelsFolder);

            string fullPathToTemplate = Expander.Model.GetTemplateFolder(Parameters, Resources.ViewModelMapperTemplate);

            foreach (var entity in App.Entities)
            {
                object parameters = new
                {
                    Entity = entity,
                    component,
                    clientComponent,
                    applicationComponent,
                };

                string result = templateService.Render(fullPathToTemplate, parameters);

                File.WriteAllText(System.IO.Path.Combine(viewModelsFolder, $"{entity.Name}ModelMapper.cs"), result);
            }
        }
    }
}
