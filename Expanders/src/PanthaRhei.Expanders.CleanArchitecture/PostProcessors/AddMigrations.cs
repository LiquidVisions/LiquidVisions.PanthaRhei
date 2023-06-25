using System;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.PostProcessors;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.PostProcessors
{
    /// <summary>
    /// A <seealso cref="PostProcessorInteractor{TExpander}"/> that handles EntityFramework migrations.
    /// </summary>
    public class AddMigrations : PostProcessorInteractor<CleanArchitectureExpander>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddMigrations"/> class.
        /// </summary>
        /// <param name="factory"><seealso cref="IDependencyFactory"/></param>
        public AddMigrations(IDependencyFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Gets a value indicating whether the <seealso cref="PostProcessorInteractor{TExpander}"/> can be executed.
        /// <seealso cref="GenerationModes"/> should be <seealso cref="GenerationModes.Deploy"/>.
        /// AND
        /// <seealso cref="App"/> should have been changed, checked by a checksum on previous generation cycle.
        /// </summary>
        public override bool Enabled => Options.Modes.HasFlag(GenerationModes.Migrate);

        /// <summary>
        /// Executes the dotnet ef migrations add cli command. Generated filenames are prefixed with 'NSCSharpGenerated'.
        /// </summary>
        public override void Execute()
        {
            string name = $"{App.Name}Generated_{DateTime.Now.Ticks}";

            string outputFolder = Expander.GetComponentOutputFolder(Expander.GetComponentByName(Resources.EntityFramework));

            CommandLine.Start($"dotnet ef migrations add {name}", outputFolder);
        }
    }
}
