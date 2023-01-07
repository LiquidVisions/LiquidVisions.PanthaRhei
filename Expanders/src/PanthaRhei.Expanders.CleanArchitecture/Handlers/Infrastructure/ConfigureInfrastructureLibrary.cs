using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Infrastructure
{
    /// <summary>
    /// Configures the dependency inversion container of the Infrastructure library.
    /// </summary>
    public class ConfigureInfrastructureLibrary : AbstractHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IWriterInteractor writer;
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureInfrastructureLibrary"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ConfigureInfrastructureLibrary(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
            : base(expander, dependencyFactory)
        {
            writer = dependencyFactory.Get<IWriterInteractor>();
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
        }

        public override int Order => 10;

        /// <inheritdoc/>
        public override void Execute()
        {
            Component component = Expander.Model.GetComponentByName(Resources.EntityFramework);
            string fullPathToBootstrapperFile = Path.Combine(projectAgent.GetComponentOutputFolder(component), Resources.DependencyInjectionBootstrapperFile);

            writer.Load(fullPathToBootstrapperFile);

            foreach (Entity entity in App.Entities)
            {
                string fullPathToTemplate = Expander.Model.GetTemplateFolder(Parameters, Resources.InfrastructureDependencyInjectionBootstrapperTemplate);
                string result = templateService.Render(fullPathToTemplate, new { Entity = entity });

                writer.AddOrReplaceMethod(result);
                writer.AppendToMethod("AddInfrastructureLayer", $"            services.Add{entity.Name}();");
            }

            writer.Save(fullPathToBootstrapperFile);
        }
    }
}
