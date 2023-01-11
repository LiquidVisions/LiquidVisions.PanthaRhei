using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Infrastructure
{
    /// <summary>
    /// Generates the DbContext class into the Infrastructure library.
    /// </summary>
    public class ExpandRepository : AbstractHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandRepository"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ExpandRepository(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
            : base(expander, dependencyFactory)
        {
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
        }

        public override int Order => 11;

        /// <inheritdoc/>
        public override void Execute()
        {
            Component component = Expander.Model.GetComponentByName(Resources.EntityFramework);
            Component applicationComponent = Expander.Model.GetComponentByName(Resources.Application); 
            string path = System.IO.Path.Combine(projectAgent.GetComponentOutputFolder(component), Resources.RepositoryFolder);
            Directory.Create(path);

            foreach (Entity entity in App.Entities)
            {
                var parameters = new
                {
                    entity,
                    component,
                    applicationComponent,
                };

                string fullPathToTemplate = Expander.Model.GetTemplateFolder(Parameters, Resources.RepositoryTemplate);
                string result = templateService.Render(fullPathToTemplate, parameters);

                string filePath = System.IO.Path.Combine(projectAgent.GetComponentOutputFolder(component), $"{entity.Name}Repository.cs");
                File.WriteAllText(filePath, result);
            }
        }
    }
}