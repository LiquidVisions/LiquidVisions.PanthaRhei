using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Domain
{
    /// <summary>
    /// Generates the <seealso cref="Entity">Entities</seealso>.
    /// </summary>
    public class ScaffoldEntities : IHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly ITemplateInteractor templateService;
        private readonly IProjectAgentInteractor projectInteractor;
        private readonly Parameters parameters;
        private readonly App app;
        private readonly Component domain;
        private readonly string entitiesFolder;
        private readonly CleanArchitectureExpander expander;
        private readonly string templateFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScaffoldEntities"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/>.</param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ScaffoldEntities(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            this.expander = expander;

            templateService = dependencyFactory.Get<ITemplateInteractor>();
            projectInteractor = dependencyFactory.Get<IProjectAgentInteractor>();
            parameters = dependencyFactory.Get<Parameters>();
            app = dependencyFactory.Get<App>();

            domain = Expander.Model.GetComponentByName(Resources.Domain);
            templateFolder = Expander.Model.GetTemplateFolder(parameters, Resources.EntityTemplate);
            string componentFolder = projectInteractor.GetComponentOutputFolder(domain);
            entitiesFolder = Path.Combine(componentFolder, Resources.DomainEntityFolder);
            dependencyFactory.Get<IDirectory>().Create(entitiesFolder);
        }

        public int Order => 2;

        public bool CanExecute => parameters.CanExecuteDefaultAndExtend();

        public string Name => nameof(ScaffoldEntities);

        public CleanArchitectureExpander Expander => expander;

        /// <inheritdoc/>
        public void Execute()
        {
            foreach (var entity in app.Entities)
            {
                string fullSavePath = Path.Combine(entitiesFolder, $"{entity.Name}.cs");
                templateService.RenderAndSave(templateFolder, new { entity }, fullSavePath);
            }
        }
    }
}
