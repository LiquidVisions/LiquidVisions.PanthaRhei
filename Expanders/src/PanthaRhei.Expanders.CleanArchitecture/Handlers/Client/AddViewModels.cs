using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Client
{
    /// <summary>
    /// Generates the ViewModels for the <seealso cref="Entity">Entities</seealso>.
    /// </summary>
    public class AddViewModels : AbstractHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddViewModels"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddViewModels(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
            : base(expander, dependencyFactory)
        {
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
        }

        public override int Order => 17;

        /// <inheritdoc/>
        public override void Execute()
        {
            Component component = Expander.Model.GetComponentByName(Resources.Client);
            string viewModelsFolder = Path.Combine(projectAgent.GetComponentOutputFolder(component), Resources.ViewModelsFolder);
            Directory.Create(viewModelsFolder);

            foreach (Entity entity in App.Entities)
            {
                var parameters = new
                {
                    component,
                    Entity = entity,
                };

                string fullPathToTemplate = Expander.Model.GetTemplateFolder(Parameters, Resources.ViewModelTemplate);
                string result = templateService.Render(fullPathToTemplate, parameters);

                string path = Path.Combine(viewModelsFolder, $"{entity.Name}ViewModel.cs");
                File.WriteAllText(path, result);
            }
        }
    }
}
