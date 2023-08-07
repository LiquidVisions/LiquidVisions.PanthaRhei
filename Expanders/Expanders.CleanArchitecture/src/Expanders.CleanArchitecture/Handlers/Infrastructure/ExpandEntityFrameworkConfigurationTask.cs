using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Infrastructure
{
    /// <summary>
    /// Configures the dependency inversion container of the Infrastructure library.
    /// </summary>
    public class ExpandEntityFrameworkConfigurationTask : IExpanderTask<CleanArchitectureExpander>
    {
        private readonly IWriter writer;
        private readonly ITemplate templateService;
        private readonly GenerationOptions options;
        private readonly App app;
        private readonly string fullPathToBootstrapperFile;
        private readonly CleanArchitectureExpander expander;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandEntityFrameworkConfigurationTask"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandEntityFrameworkConfigurationTask(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            writer = dependencyFactory.Get<IWriter>();
            templateService = dependencyFactory.Get<ITemplate>();
            options = dependencyFactory.Get<GenerationOptions>();
            app = dependencyFactory.Get<App>();

            Component component = Expander.GetComponentByName(Resources.EntityFramework);
            string componentOutputPath = expander.GetComponentOutputFolder(component);
            fullPathToBootstrapperFile = Path.Combine(componentOutputPath, Resources.DependencyInjectionBootstrapperFile);
        }

        /// <inheritdoc/>
        public int Order => 10;

        /// <inheritdoc/>
        public string Name => nameof(ExpandEntityFrameworkConfigurationTask);

        /// <inheritdoc/>
        public CleanArchitectureExpander Expander => expander;

        /// <inheritdoc/>
        public bool Enabled => options.CanExecuteDefaultAndExtend();

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
