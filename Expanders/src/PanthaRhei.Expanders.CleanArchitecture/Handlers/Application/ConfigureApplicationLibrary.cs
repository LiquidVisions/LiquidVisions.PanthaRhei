using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Application
{
    /// <summary>
    /// Configures the dependency inversion container of the Application library.
    /// </summary>
    public class ConfigureApplicationLibrary : AbstractHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IWriterInteractor writer;
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureApplicationLibrary"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ConfigureApplicationLibrary(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
            : base(expander, dependencyFactory)
        {
            writer = dependencyFactory.Get<IWriterInteractor>();
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
        }

        public override int Order => 7;

        /// <inheritdoc/>
        public override void Execute()
        {
            Component component = Expander.Model.GetComponentByName(Resources.Application);
            string path = Path.Combine(projectAgent.GetComponentOutputFolder(component), Resources.DependencyInjectionBootstrapperFile);

            writer.Load(path);

            foreach (Entity entity in App.Entities)
            {
                string fullPathToTemplate = Expander.Model.GetTemplateFolder(Parameters, Resources.ApplicationDependencyInjectionBootstrapperTemplate);
                string result = templateService.Render(fullPathToTemplate, new { Entity = entity });

                writer.AddOrReplaceMethod(result);
                writer.AppendToMethod("AddApplicationLayer", $"            services.Add{entity.Name}();");

                string pluralizedName = entity.Name.Pluralize();
                string ns = component.GetComponentNamespace(App);

                writer.AddNameSpace($"{ns}.Boundaries.{pluralizedName}");
                writer.AddNameSpace($"{ns}.Interactors.{pluralizedName}");
                writer.AddNameSpace($"{ns}.Mappers.{pluralizedName}");
                writer.AddNameSpace($"{Expander.Model.Name}.Client.RequestModels.{pluralizedName}");
                writer.AddNameSpace($"{ns}.Validators.{pluralizedName}");
                writer.AddNameSpace($"{ns}.Gateways");
            }

            writer.Save(path);
        }
    }
}
