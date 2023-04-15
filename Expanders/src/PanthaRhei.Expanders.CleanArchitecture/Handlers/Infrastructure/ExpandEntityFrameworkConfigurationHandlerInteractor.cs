using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Infrastructure
{
    /// <summary>
    /// Configures the dependency inversion container of the Infrastructure library.
    /// </summary>
    public class ExpandEntityFrameworkConfigurationHandlerInteractor : IExpanderHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IWriterInteractor writer;
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;
        private readonly ExpandRequestModel expandRequestModel;
        private readonly App app;
        private readonly string fullPathToBootstrapperFile;
        private readonly CleanArchitectureExpander expander;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandEntityFrameworkConfigurationHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ExpandEntityFrameworkConfigurationHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            this.expander = expander;

            writer = dependencyFactory.Get<IWriterInteractor>();
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
            expandRequestModel = dependencyFactory.Get<ExpandRequestModel>();
            app = dependencyFactory.Get<App>();

            Component component = Expander.Model.GetComponentByName(Resources.EntityFramework);
            string componentOutputPath = projectAgent.GetComponentOutputFolder(component);
            fullPathToBootstrapperFile = Path.Combine(componentOutputPath, Resources.DependencyInjectionBootstrapperFile);
        }

        public int Order => 10;

        public string Name => nameof(ExpandEntityFrameworkConfigurationHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => expandRequestModel.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public void Execute()
        {
            writer.Load(fullPathToBootstrapperFile);

            foreach (Entity entity in app.Entities)
            {
                string fullPathToTemplate = Expander.Model.GetPathToTemplate(expandRequestModel, Resources.InfrastructureDependencyInjectionBootstrapperTemplate);
                string result = templateService.Render(fullPathToTemplate, new { Entity = entity });

                writer.AddOrReplaceMethod(result);
                writer.AppendToMethod("AddInfrastructureLayer", $"            services.Add{entity.Name}();");
            }

            writer.Save(fullPathToBootstrapperFile);
        }
    }
}
