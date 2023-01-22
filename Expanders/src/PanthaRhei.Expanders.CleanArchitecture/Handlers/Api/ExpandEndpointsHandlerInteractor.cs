using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using IO = System.IO;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api
{
    /// <summary>
    /// Adds an endpoint to the API.
    /// </summary>
    public class ExpandEndpointsHandlerInteractor : IExpanderHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IWriterInteractor writer;
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;
        private readonly CleanArchitectureExpander expander;
        private readonly Parameters parameters;
        private readonly IDirectory directory;
        private readonly App app;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandEndpointsHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ExpandEndpointsHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            this.expander = expander;

            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            directory = dependencyFactory.Get<IDirectory>();
            parameters = dependencyFactory.Get<Parameters>();
            writer = dependencyFactory.Get<IWriterInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
            app = dependencyFactory.Get<App>();
        }

        public int Order => 16;

        public string Name => nameof(ExpandEndpointsHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => parameters.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public virtual void Execute()
        {
            Component component = Expander.Model.GetComponentByName(Resources.Api);

            string folder = IO.Path.Combine(projectAgent.GetComponentOutputFolder(component), Resources.EndpointFolder);
            directory.Create(folder);

            string fullPathToTemplate = Expander.Model.GetTemplateFolder(parameters, Resources.EndpointTemplate);

            foreach (Entity entity in app.Entities)
            {
                GenerateAndSaveOutput(component, folder, entity, fullPathToTemplate);

                ModifyBootstrapperFile(component, entity);
            }
        }

        private void ModifyBootstrapperFile(Component component, Entity entity)
        {
            string bootstrapperFile = IO.Path.Combine(projectAgent.GetComponentOutputFolder(component), Resources.DependencyInjectionBootstrapperFile);

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
            Component clientComponent = Expander.Model.GetComponentByName(Resources.Client);
            Component applicationComponent = Expander.Model.GetComponentByName(Resources.Application);

            var templateModel = new
            {
                applicationComponent,
                clientComponent,
                component,
                Entity = endpoint,
            };

            string fullPath = IO.Path.Combine(destinationFolder, $"{endpoint.Name}{Resources.EndpointFolder}.cs");
            templateService.RenderAndSave(fullPathToTemplate, templateModel, fullPath);
        }
    }
}
