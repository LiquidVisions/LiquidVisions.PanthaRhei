using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api
{
    /// <summary>
    /// Adds swagger capabilities to the outputted project.
    /// </summary>
    public class AddSwagger : IHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IWriterInteractor writer;
        private readonly Parameters parameters;
        private readonly IProjectAgentInteractor projectAgent;
        private readonly CleanArchitectureExpander expander;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddSwagger"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddSwagger(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            writer = dependencyFactory.Get<IWriterInteractor>();
            parameters = dependencyFactory.Get<Parameters>();
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
            this.expander = expander;
        }

        public int Order => 12;

        public string Name => nameof(AddSwagger);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => parameters.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public void Execute()
        {
            string matchServices = "return services;";
            string matchApp = "app.Run();";

            Component component = Expander.Model.GetComponentByName(Resources.Api);

            string path = System.IO.Path.Combine(projectAgent.GetComponentOutputFolder(component), Resources.DependencyInjectionBootstrapperFile);

            writer.Load(path);

            writer.WriteAt(matchServices, "services.AddEndpointsApiExplorer();");
            writer.WriteAt(matchServices, "services.AddSwaggerGen();");
            writer.WriteAt(matchServices, string.Empty);

            writer.WriteAt(matchApp, "app.UseSwagger();");
            writer.WriteAt(matchApp, "app.UseSwaggerUI();");
            writer.WriteAt(matchApp, string.Empty);

            writer.Save(path);
        }
    }
}
