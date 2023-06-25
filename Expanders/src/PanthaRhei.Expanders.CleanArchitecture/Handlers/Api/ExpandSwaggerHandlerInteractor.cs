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
    public class ExpandSwaggerHandlerInteractor : IExpanderHandlerInteractor<CleanArchitectureExpander>
    {
        private readonly IWriterInteractor writer;
        private readonly GenerationOptions options;
        private readonly CleanArchitectureExpander expander;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandSwaggerHandlerInteractor"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public ExpandSwaggerHandlerInteractor(CleanArchitectureExpander expander, IDependencyFactoryInteractor dependencyFactory)
        {
            writer = dependencyFactory.Get<IWriterInteractor>();
            options = dependencyFactory.Get<GenerationOptions>();
            this.expander = expander;
        }

        public int Order => 12;

        public string Name => nameof(ExpandSwaggerHandlerInteractor);

        public CleanArchitectureExpander Expander => expander;

        public bool CanExecute => options.CanExecuteDefaultAndExtend();

        /// <inheritdoc/>
        public void Execute()
        {
            string matchServices = "return services;";
            string matchApp = "app.Run();";

            Component component = Expander.GetComponentByName(Resources.Api);

            string path = System.IO.Path.Combine(expander.GetComponentOutputFolder(component), Resources.DependencyInjectionBootstrapperFile);

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
