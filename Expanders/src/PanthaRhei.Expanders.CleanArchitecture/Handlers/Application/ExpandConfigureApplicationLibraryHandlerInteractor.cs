using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Application
{
    /// <summary>
    /// Configures the dependency inversion container of the Application library.
    /// </summary>
    public class ExpandConfigureApplicationLibraryHandlerInteractor : IExpanderTask<CleanArchitectureExpander>
    {
        private readonly IWriterInteractor writer;
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
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandConfigureApplicationLibraryHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            writer = dependencyFactory.Get<IWriterInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
            options = dependencyFactory.Get<GenerationOptions>();
            app = dependencyFactory.Get<App>();

            component = Expander.GetComponentByName(Resources.Application);

            fullPathToTemplate = Expander.Model.GetPathToTemplate(options, Resources.ApplicationDependencyInjectionBootstrapperTemplate);

            string fullPathToComponent = expander.GetComponentOutputFolder(component);
            fullPathToBootstrapperFile = Path.Combine(fullPathToComponent, Resources.DependencyInjectionBootstrapperFile);
        }

        public int Order => 7;

        public string Name => nameof(ExpandConfigureApplicationLibraryHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool Enabled => options.CanExecuteDefaultAndExtend();

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
