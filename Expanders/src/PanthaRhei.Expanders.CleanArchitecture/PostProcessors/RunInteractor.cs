using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.PostProcessors;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.PostProcessors
{
    public class RunInteractor : PostProcessor<CleanArchitectureExpander>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunInteractor "/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public RunInteractor(IDependencyFactory dependencyFactory)
            : base(dependencyFactory)
        {
        }

        public string Name => nameof(RunInteractor);

        public override bool Enabled => Options.Modes.HasFlag(GenerationModes.Run);

        public override void Execute()
        {
            Component apiComponent = Expander.GetComponentByName(Resources.Api);
            string folder = Expander.GetComponentOutputFolder(apiComponent);

            Logger.Info("STARTING WEBSERVER...");

            CommandLine.Start("dotnet run", folder);
        }
    }
}
