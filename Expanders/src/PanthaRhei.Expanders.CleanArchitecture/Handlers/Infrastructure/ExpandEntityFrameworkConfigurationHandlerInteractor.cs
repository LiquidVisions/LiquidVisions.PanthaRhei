using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Interactors;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Domain.Interactors.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Infrastructure
{
    /// <summary>
    /// Configures the dependency inversion container of the Infrastructure library.
    /// </summary>
    public class ExpandEntityFrameworkConfigurationHandlerInteractor : IExpanderHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IWriterInteractor writer;
        private readonly ITemplateInteractor templateService;
        private readonly GenerationOptions options;
        private readonly App app;
        private readonly string fullPathToBootstrapperFile;
        private readonly CleanArchitectureExpander expander;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandEntityFrameworkConfigurationHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandEntityFrameworkConfigurationHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            writer = dependencyFactory.Get<IWriterInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
            options = dependencyFactory.Get<GenerationOptions>();
            app = dependencyFactory.Get<App>();

            Component component = Expander.GetComponentByName(Resources.EntityFramework);
            string componentOutputPath = expander.GetComponentOutputFolder(component);
            fullPathToBootstrapperFile = Path.Combine(componentOutputPath, Resources.DependencyInjectionBootstrapperFile);
        }

        public int Order => 10;

        public string Name => nameof(ExpandEntityFrameworkConfigurationHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => options.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public void Execute()
        {
            writer.Load(fullPathToBootstrapperFile);

            foreach (Entity entity in app.Entities)
            {
                string fullPathToTemplate = Expander.Model.GetPathToTemplate(options, Resources.InfrastructureDependencyInjectionBootstrapperTemplate);
                string result = templateService.Render(fullPathToTemplate, new { Entity = entity });

                writer.AddOrReplaceMethod(result);
                writer.AppendToMethod("AddInfrastructureLayer", $"            services.Add{entity.Name}();");
            }

            writer.Save(fullPathToBootstrapperFile);
        }
    }
}
