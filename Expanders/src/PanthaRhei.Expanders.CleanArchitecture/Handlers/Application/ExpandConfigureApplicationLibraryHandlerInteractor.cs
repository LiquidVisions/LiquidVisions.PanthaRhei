using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Application
{
    /// <summary>
    /// Configures the dependency inversion container of the Application library.
    /// </summary>
    public class ExpandConfigureApplicationLibraryHandlerInteractor : IExpanderHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IWriterInteractor writer;
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;
        private readonly GenerationOptions options;
        private readonly App app;
        private readonly Component component;
        private readonly CleanArchitectureExpander expander;
        private readonly string fullPathToTemplate;
        private readonly string fullPathToBootstrapperFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandConfigureApplicationLibraryHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ExpandConfigureApplicationLibraryHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            this.expander = expander;

            writer = dependencyFactory.Get<IWriterInteractor>();
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
            options = dependencyFactory.Get<GenerationOptions>();
            app = dependencyFactory.Get<App>();

            component = Expander.Model.GetComponentByName(Resources.Application);

            fullPathToTemplate = Expander.Model.GetPathToTemplate(options, Resources.ApplicationDependencyInjectionBootstrapperTemplate);

            string fullPathToComponent = projectAgent.GetComponentOutputFolder(component);
            fullPathToBootstrapperFile = Path.Combine(fullPathToComponent, Resources.DependencyInjectionBootstrapperFile);
        }

        public int Order => 7;

        public string Name => nameof(ExpandConfigureApplicationLibraryHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => options.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public void Execute()
        {
            writer.Load(fullPathToBootstrapperFile);

            foreach (Entity entity in app.Entities)
            {
                string result = templateService.Render(fullPathToTemplate, new { Entity = entity });

                writer.AddOrReplaceMethod(result);
                writer.AppendToMethod("AddApplicationLayer", $"            services.Add{entity.Name}();");

                string pluralizedName = entity.Name.Pluralize();
                string ns = component.GetComponentNamespace(app);

                writer.AddNameSpace($"{ns}.Boundaries.{pluralizedName}");
                writer.AddNameSpace($"{ns}.Interactors.{pluralizedName}");
                writer.AddNameSpace($"{ns}.Mappers.{pluralizedName}");
                writer.AddNameSpace($"{component.GetComponentNamespace(app)}.RequestModels.{pluralizedName}");
                writer.AddNameSpace($"{ns}.Validators.{pluralizedName}");
                writer.AddNameSpace($"{ns}.Gateways");
            }

            writer.Save(fullPathToBootstrapperFile);
        }
    }
}
