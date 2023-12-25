using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Domain.Tasks
{
    /// <summary>
    /// Generates the <seealso cref="Entity">Entities</seealso>.
    /// </summary>
    public class ExpandEntitiesTask : IExpanderTask<DomainExpander>
    {
        private readonly ITemplate templateService;
        private readonly GenerationOptions options;
        private readonly App app;
        private readonly Component domain;
        private readonly string entitiesFolder;
        private readonly IDirectory directory;
        private readonly DomainExpander expander;
        private readonly string templateFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandEntitiesTask"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="DomainExpander"/>.</param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandEntitiesTask(DomainExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            templateService = dependencyFactory.Resolve<ITemplate>();
            options = dependencyFactory.Resolve<GenerationOptions>();
            app = dependencyFactory.Resolve<App>();
            directory = dependencyFactory.Resolve<IDirectory>();

            domain = Expander.GetComponentByName(Resources.Domain);
            templateFolder = Expander.Model.GetPathToTemplate(options, Resources.EntityTemplate);
            string componentFolder = expander.GetComponentOutputFolder(domain);
            entitiesFolder = Path.Combine(componentFolder, Resources.DomainEntityFolder);
        }

        /// <inheritdoc/>
        public int Order => 2;

        /// <inheritdoc/>
        public bool Enabled => options.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public string Name => nameof(ExpandEntitiesTask);

        /// <inheritdoc/>
        public DomainExpander Expander => expander;

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
