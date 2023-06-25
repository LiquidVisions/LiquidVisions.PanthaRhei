using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.PostProcessors;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.PostProcessors
{
    /// <summary>
    /// A <seealso cref="PostProcessorInteractor{TExpander}"/> updates database based on entityframework specifications.
    /// </summary>
    public class UpdateDatabase : PostProcessorInteractor<CleanArchitectureExpander>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDatabase"/> class.
        /// </summary>
        /// <param name="factory"><seealso cref="IDependencyFactory"/></param>
        public UpdateDatabase(IDependencyFactory factory)
            : base(factory)
        {
        }

        /// <inheritdoc/>
        public override bool Enabled => Options.Modes.HasFlag(GenerationModes.Migrate);

        /// <inheritdoc/>
        public override void Execute()
        {
            Component component = Expander.GetComponentByName(Resources.EntityFramework);

            CommandLine.Start("dotnet ef database update", Expander.GetComponentOutputFolder(component));
        }
    }
}
