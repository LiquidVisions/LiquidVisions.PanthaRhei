using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Domain
{
    /// <summary>
    /// Generates the <seealso cref="Entity">Entities</seealso>.
    /// </summary>
    public class ScaffoldEntities : AbstractHandler<CleanArchitectureExpander>
    {
        private readonly ITemplateService templateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScaffoldEntities"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/>.</param>
        /// <param name="dependencyResolver"><seealso cref="IDependencyResolver"/></param>
        public ScaffoldEntities(CleanArchitectureExpander expander, IDependencyResolver dependencyResolver)
            : base(expander, dependencyResolver)
        {
            this.templateService = dependencyResolver.Get<ITemplateService>();
        }

        public override int Order => 2;

        public override bool CanExecute => Parameters.GenerationMode.HasFlag(GenerationModes.Default) || Parameters.GenerationMode.HasFlag(GenerationModes.Extend);

        /// <inheritdoc/>
        public override void Execute()
        {
            Component domain = Expander.Model.GetComponentByName(Resources.Domain);
            string entitiesFolder = Path.Combine(ProjectAgent.GetComponentOutputFolder(domain), Resources.DomainEntityFolder);
            DirectoryService.Create(entitiesFolder);

            foreach (var entity in App.Entities)
            {
                string result = templateService.Render(
                    Path.Combine(Parameters.ExpandersFolder, Expander.Model.Name, Expander.Model.TemplateFolder, $"{Resources.EntityTemplate}.template"),
                    new
                    {
                        Entity = entity,
                    });

                FileService.WriteAllText(System.IO.Path.Combine(entitiesFolder, $"{entity.Name}.cs"), result);
            }
        }
    }
}
