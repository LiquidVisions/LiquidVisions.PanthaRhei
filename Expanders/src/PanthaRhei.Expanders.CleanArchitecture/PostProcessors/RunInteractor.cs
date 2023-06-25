using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.PostProcessors;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.PostProcessors
{
    public class RunInteractor : PostProcessorInteractor<CleanArchitectureExpander>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunInteractor "/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public RunInteractor(IDependencyFactoryInteractor dependencyFactory)
            : base(dependencyFactory)
        {
        }

        public string Name => nameof(RunInteractor);

        public override bool CanExecute => Options.Modes.HasFlag(GenerationModes.Run);

        public override void Execute()
        {
            Component apiComponent = Expander.GetComponentByName(Resources.Api);
            string folder = Expander.GetComponentOutputFolder(apiComponent);

            Logger.Info("STARTING WEBSERVER...");

            CommandLine.Start("dotnet run", folder);
        }
    }
}
