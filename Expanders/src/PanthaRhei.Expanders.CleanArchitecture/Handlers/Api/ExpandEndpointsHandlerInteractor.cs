using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;
using IO = System.IO;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api
{
    /// <summary>
    /// Adds an endpoint to the API.
    /// </summary>
    public class ExpandEndpointsHandlerInteractor : IExpanderTask<CleanArchitectureExpander>
    {
        private readonly IWriter writer;
        private readonly ITemplate templateService;
        private readonly CleanArchitectureExpander expander;
        private readonly GenerationOptions options;
        private readonly IDirectory directory;
        private readonly App app;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandEndpointsHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandEndpointsHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            this.expander = expander;

            directory = dependencyFactory.Get<IDirectory>();
            options = dependencyFactory.Get<GenerationOptions>();
            writer = dependencyFactory.Get<IWriter>();
            templateService = dependencyFactory.Get<ITemplate>();
            app = dependencyFactory.Get<App>();
        }

        public int Order => 16;

        public string Name => nameof(ExpandEndpointsHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool Enabled => options.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public virtual void Execute()
        {
            Component component = Expander.GetComponentByName(Resources.Api);

            string folder = IO.Path.Combine(expander.GetComponentOutputFolder(component), Resources.EndpointFolder);
            directory.Create(folder);

            string fullPathToTemplate = Expander.Model.GetPathToTemplate(options, Resources.EndpointTemplate);

            foreach (Entity entity in app.Entities)
            {
                GenerateAndSaveOutput(component, folder, entity, fullPathToTemplate);

                ModifyBootstrapperFile(component, entity);
            }
        }

        private void ModifyBootstrapperFile(Component component, Entity entity)
        {
            string bootstrapperFile = IO.Path.Combine(expander.GetComponentOutputFolder(component), Resources.DependencyInjectionBootstrapperFile);

            writer.Load(bootstrapperFile);

            int index = writer.IndexOf("return services;") - 1;
            writer.WriteAt(index, string.Empty);
            writer.WriteAt(index + 1, $"            services.Add{entity.Name}Elements();");

            index = writer.IndexOf("app.Run();") - 1;
            writer.WriteAt(index, string.Empty);
            writer.WriteAt(index + 1, $"            app.Use{entity.Name}Endpoints();");

            writer.Save(bootstrapperFile);
        }

        private void GenerateAndSaveOutput(Component component, string destinationFolder, Entity endpoint, string fullPathToTemplate)
        {
            Component applicationComponent = Expander.GetComponentByName(Resources.Application);

            var templateModel = new
            {
                applicationComponent,
                component,
                Entity = endpoint,
            };

            string fullPath = IO.Path.Combine(destinationFolder, $"{endpoint.Name}{Resources.EndpointFolder}.cs");
            templateService.RenderAndSave(fullPathToTemplate, templateModel, fullPath);
        }
    }
}
