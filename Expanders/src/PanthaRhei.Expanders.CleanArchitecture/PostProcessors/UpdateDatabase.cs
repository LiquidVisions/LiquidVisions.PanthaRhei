using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.PostProcessors;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.PostProcessors
{
    /// <summary>
    /// A <seealso cref="PostProcessorInteractor{TExpander}"/> updates database based on entityframework specifications.
    /// </summary>
    public class UpdateDatabase : PostProcessorInteractor<CleanArchitectureExpander>
    {
        private readonly IProjectAgentInteractor projectAgentInteractor;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDatabase"/> class.
        /// </summary>
        /// <param name="factory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public UpdateDatabase(IDependencyFactoryInteractor factory)
            : base(factory)
        {
            projectAgentInteractor = factory.Get<IProjectAgentInteractor>();
        }

        /// <inheritdoc/>
        public override bool CanExecute => Options.Modes.HasFlag(GenerationModes.Migrate);

        /// <inheritdoc/>
        public override void Execute()
        {
            Component component = Expander.Model.GetComponentByName(Resources.EntityFramework);

            CommandLine.Start("dotnet ef database update", projectAgentInteractor.GetComponentOutputFolder(component));
        }
    }
}
