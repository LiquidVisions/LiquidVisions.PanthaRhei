using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
using IO = System.IO;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api
{
    /// <summary>
    /// Adds an endpoint to the API.
    /// </summary>
    public class AddEndpoints : AbstractHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IWriterInteractor writer;
        private readonly IProjectAgentInteractor projectAgent;
        private readonly ITemplateInteractor templateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddEndpoints"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddEndpoints(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
            : base(expander, dependencyFactory)
        {
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            writer = dependencyFactory.Get<IWriterInteractor>();
            templateService = dependencyFactory.Get<ITemplateInteractor>();
        }

        public override int Order => 16;

        /// <inheritdoc/>
        public override void Execute()
        {
            Component component = Expander.Model.GetComponentByName(Resources.Api);

            string folder = IO.Path.Combine(projectAgent.GetComponentOutputFolder(component), Resources.EndpointFolder);
            Directory.Create(folder);

            string fullPathToTemplate = Expander.Model.GetTemplateFolder(Parameters, Resources.EndpointTemplate);

            foreach (Entity endpoint in App.Entities)
            {
                GenerateAndSaveOutput(component, folder, endpoint, fullPathToTemplate);

                ModifyBootstrapperFile(component, endpoint);
            }
        }

        private void ModifyBootstrapperFile(Component component, Entity endpoint)
        {
            string bootstrapperFile = IO.Path.Combine(projectAgent.GetComponentOutputFolder(component), Resources.DependencyInjectionBootstrapperFile);

            writer.Load(bootstrapperFile);

            int index = writer.IndexOf("return services;") - 1;
            writer.WriteAt(index, string.Empty);
            writer.WriteAt(index + 1, $"            services.Add{endpoint.Name}Elements();");

            index = writer.IndexOf("app.Run();") - 1;
            writer.WriteAt(index, string.Empty);
            writer.WriteAt(index + 1, $"            app.Use{endpoint.Name}Endpoints();");

            writer.Save(bootstrapperFile);
        }

        private void GenerateAndSaveOutput(Component component, string destinationFolder, Entity endpoint, string fullPathToTemplate)
        {
            Component clientComponent = Expander.Model.GetComponentByName(Resources.Client);
            Component applicationComponent = Expander.Model.GetComponentByName(Resources.Application);

            var parameters = new
            {
                applicationComponent,
                clientComponent,
                component,
                Entity = endpoint,
            };

            string result = templateService.Render(fullPathToTemplate, parameters);

            string path = IO.Path.Combine(destinationFolder, $"{endpoint.Name}{Resources.EndpointFolder}.cs");
            File.WriteAllText(path, result);
        }
    }
}
