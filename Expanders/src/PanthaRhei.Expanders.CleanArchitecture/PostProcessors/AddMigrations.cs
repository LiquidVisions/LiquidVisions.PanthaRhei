using System;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.PostProcessors;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.PostProcessors
{
    /// <summary>
    /// A <seealso cref="PostProcessorInteractor{TExpander}"/> that handles EntityFramework migrations.
    /// </summary>
    public class AddMigrations : PostProcessorInteractor<CleanArchitectureExpander>
    {
        private readonly IProjectAgentInteractor projectAgentInteractor;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddMigrations"/> class.
        /// </summary>
        /// <param name="factory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public AddMigrations(IDependencyFactoryInteractor factory)
            : base(factory)
        {
            projectAgentInteractor = factory.Get<IProjectAgentInteractor>();
        }

        /// <summary>
        /// Gets a value indicating whether the <seealso cref="PostProcessorInteractor{TExpander}"/> can be executed.
        /// <seealso cref="GenerationModes"/> should be <seealso cref="GenerationModes.Deploy"/>.
        /// AND
        /// <seealso cref="App"/> should have been changed, checked by a checksum on previous generation cycle.
        /// </summary>
        public override bool CanExecute => Parameters.GenerationMode.HasFlag(GenerationModes.Migrate);

        /// <summary>
        /// Executes the dotnet ef migrations add cli command. Generated filenames are prefixed with 'NSCSharpGenerated'.
        /// </summary>
        public override void Execute()
        {
            string name = $"{App.Name}Generated_{DateTime.Now.Ticks}";

            string outputFolder = projectAgentInteractor.GetComponentOutputFolder(Expander.Model.GetComponentByName(Resources.EntityFramework));

            CommandLine.Start($"dotnet ef migrations add {name}", outputFolder);
        }
    }
}
