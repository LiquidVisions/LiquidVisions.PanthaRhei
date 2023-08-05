using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.PostProcessors;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.PostProcessors
{
    /// <summary>
    /// Starts the generated API service using dotnet run command.
    /// </summary>
    public class RunWebserviceTask : PostProcessor<CleanArchitectureExpander>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunWebserviceTask "/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public RunWebserviceTask(IDependencyFactory dependencyFactory)
            : base(dependencyFactory)
        {
        }

        /// <inheritdoc/>
        public string Name => nameof(RunWebserviceTask);

        /// <inheritdoc/>
        public override bool Enabled => Options.Modes.HasFlag(GenerationModes.Run);

        /// <inheritdoc/>
        public override void Execute()
        {
            Component apiComponent = Expander.GetComponentByName(Resources.Api);
            string folder = Expander.GetComponentOutputFolder(apiComponent);

            Logger.Info("STARTING WEBSERVER...");

            CommandLine.Start("dotnet run", folder);
        }
    }
}
