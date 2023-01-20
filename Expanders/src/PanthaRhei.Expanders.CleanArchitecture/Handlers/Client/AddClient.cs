using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Client
{
    /// <summary>
    /// a <seealso cref="AbstractHandlerInteractor{CleanArchitectureExpander}"/> that adds the request models to the output project.
    /// </summary>
    public class AddClient : IHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly ITemplateInteractor templateService;
        private readonly IProjectAgentInteractor projectAgent;
        private readonly Parameters parameters;
        private readonly App app;
        private readonly Component component;
        private readonly string fullPathToComponentFolder;
        private readonly CleanArchitectureExpander expander;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddClient"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddClient(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            this.expander = expander;

            templateService = dependencyFactory.Get<ITemplateInteractor>();
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            parameters = dependencyFactory.Get<Parameters>();
            app = dependencyFactory.Get<App>();

            component = Expander.Model.GetComponentByName(Resources.Client);
            fullPathToComponentFolder = projectAgent.GetComponentOutputFolder(component);

        }

        public int Order => 19;

        public string Name => nameof(AddClient);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => parameters.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public void Execute()
        {
            foreach (Entity entity in app.Entities)
            {
                var templateModel = new
                {
                    entity,
                    component,
                };

                string fullPathToOutputFile = Path.Combine(fullPathToComponentFolder, $"{entity.Name}Client.cs");
                string fullPathToTemplate = Expander.Model.GetTemplateFolder(parameters, Resources.ClientTemplate);
                templateService.RenderAndSave(fullPathToTemplate, templateModel, fullPathToOutputFile);
            }
        }
    }
}
