using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Handlers;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api
{
    /// <summary>
    /// Adds swagger capabilities to the outputted project.
    /// </summary>
    public class AddSwagger : AbstractHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IWriterInteractor writer;
        private readonly IProjectAgentInteractor projectAgent;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddSwagger"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddSwagger(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
            : base(expander, dependencyFactory)
        {
            writer = dependencyFactory.Get<IWriterInteractor>();
            projectAgent = dependencyFactory.Get<IProjectAgentInteractor>();
        }

        public override int Order => 12;

        /// <inheritdoc/>
        public override void Execute()
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
