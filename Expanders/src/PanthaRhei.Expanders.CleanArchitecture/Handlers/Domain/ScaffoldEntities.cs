﻿using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Domain
{
    /// <summary>
    /// Generates the <seealso cref="Entity">Entities</seealso>.
    /// </summary>
    public class ScaffoldEntities : AbstractHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly ITemplateService templateService;
        private readonly IProjectAgentInteractor projectInteractor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScaffoldEntities"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/>.</param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ScaffoldEntities(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
            : base(expander, dependencyFactory)
        {
            this.templateService = dependencyFactory.Get<ITemplateService>();
            this.projectInteractor = dependencyFactory.Get<IProjectAgentInteractor>();
        }

        public override int Order => 2;

        public override bool CanExecute => Parameters.GenerationMode.HasFlag(GenerationModes.Default) || Parameters.GenerationMode.HasFlag(GenerationModes.Extend);

        /// <inheritdoc/>
        public override void Execute()
        {
            Component domain = Expander.Model.GetComponentByName(Resources.Domain);
            string entitiesFolder = Path.Combine(projectInteractor.GetComponentOutputFolder(domain), Resources.DomainEntityFolder);
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
