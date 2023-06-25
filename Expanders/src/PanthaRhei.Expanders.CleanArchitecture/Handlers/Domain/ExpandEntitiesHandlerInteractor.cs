using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Domain.Interactors.Templates;
using LiquidVisions.PanthaRhei.Domain.IO;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Domain
{
    /// <summary>
    /// Generates the <seealso cref="Entity">Entities</seealso>.
    /// </summary>
    public class ExpandEntitiesHandlerInteractor : IExpanderTask<CleanArchitectureExpander>
    {
        private readonly ITemplateInteractor templateService;
        private readonly GenerationOptions options;
        private readonly App app;
        private readonly Component domain;
        private readonly string entitiesFolder;
        private readonly IDirectory directory;
        private readonly CleanArchitectureExpander expander;
        private readonly string templateFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandEntitiesHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/>.</param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandEntitiesHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            templateService = dependencyFactory.Get<ITemplateInteractor>();
            options = dependencyFactory.Get<GenerationOptions>();
            app = dependencyFactory.Get<App>();
            directory = dependencyFactory.Get<IDirectory>();

            domain = Expander.GetComponentByName(Resources.Domain);
            templateFolder = Expander.Model.GetPathToTemplate(options, Resources.EntityTemplate);
            string componentFolder = expander.GetComponentOutputFolder(domain);
            entitiesFolder = Path.Combine(componentFolder, Resources.DomainEntityFolder);
        }

        public int Order => 2;

        public bool Enabled => options.CanExecuteDefaultAndExtend();

        public string Name => nameof(ExpandEntitiesHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        /// <inheritdoc/>
        public void Execute()
        {
            directory.Create(entitiesFolder);

            foreach (var entity in app.Entities)
            {
                string fullSavePath = Path.Combine(entitiesFolder, $"{entity.Name}.cs");
                templateService.RenderAndSave(templateFolder, new { entity }, fullSavePath);
            }
        }
    }
}
