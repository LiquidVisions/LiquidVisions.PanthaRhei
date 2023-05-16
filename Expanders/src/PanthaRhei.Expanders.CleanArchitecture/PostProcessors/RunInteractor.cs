using System;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.PostProcessors;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.PostProcessors
{
    public class RunInteractor : PostProcessorInteractor<CleanArchitectureExpander>
    {
        private readonly IProjectAgentInteractor projectAgentInteractor;

        /// <summary>
        /// Initializes a new instance of the <see cref="RunInteractor "/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public RunInteractor(IDependencyFactoryInteractor dependencyFactory)
            : base(dependencyFactory)
        {
            projectAgentInteractor = dependencyFactory.Get<IProjectAgentInteractor>();
        }

        public string Name => nameof(RunInteractor);

        public override bool CanExecute => true;

        public override void Execute()
        {
            Component apiComponent = Expander.Model.GetComponentByName(Resources.Api);
            string folder = projectAgentInteractor.GetComponentOutputFolder(apiComponent);

            Logger.Info("STARTING WEBSERVER...");

            CommandLine.Start("dotnet run", folder);
        }
    }
}
