using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Handlers.Api
{
    /// <summary>
    /// Adds swagger capabilities to the outputted project.
    /// </summary>
    public class ExpandSwaggerTask : IExpanderTask<CleanArchitectureExpander>
    {
        private readonly IWriter writer;
        private readonly GenerationOptions options;
        private readonly CleanArchitectureExpander expander;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandSwaggerTask"/> class.
        /// </summary>
        /// <param name="expander"><seealso cref="CleanArchitectureExpander"/></param>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public ExpandSwaggerTask(CleanArchitectureExpander expander, IDependencyFactory dependencyFactory)
        {
            writer = dependencyFactory.Get<IWriter>();
            options = dependencyFactory.Get<GenerationOptions>();
            this.expander = expander;
        }

        /// <inheritdoc/>
        public int Order => 12;

        /// <inheritdoc/>
        public string Name => nameof(ExpandSwaggerTask);

        /// <inheritdoc/>
        public CleanArchitectureExpander Expander => expander;

        /// <inheritdoc/>
        public bool Enabled => options.CanExecuteDefaultAndExtend();

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
