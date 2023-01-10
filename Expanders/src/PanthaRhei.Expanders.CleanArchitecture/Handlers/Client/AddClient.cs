using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Client
{
    /// <summary>
    /// a <seealso cref="AbstractHandlerInteractor{CleanArchitectureExpander}"/> that adds the request models to the output project.
    /// </summary>
    public class AddClient : AbstractHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly ITemplateInteractor templateService;
        private readonly IProjectAgentInteractor projectAgent;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddClient"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddClient(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
            : base(expander, dependencyFactory)
        {
            templateService = dependencyFactory.Get<ITemplateInteractor>();
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
        }

        public override int Order => 19;

        /// <inheritdoc/>
        public override void Execute()
        {
            Component component = Expander.Model.GetComponentByName(Resources.Client);
            string fullPathToComponentFolder = projectAgent.GetComponentOutputFolder(component);

            foreach (Entity entity in App.Entities)
            {
                var parameters = new
                {
                    entity,
                    component,
                };

                string fullPathToTemplate = Expander.Model.GetTemplateFolder(Parameters, Resources.ClientTemplate);
                string result = templateService.Render(fullPathToTemplate, parameters);

                string path = Path.Combine(fullPathToComponentFolder, $"{entity.Name}Client.cs");
                File.WriteAllText(path, result);
            }
        }
    }
}
