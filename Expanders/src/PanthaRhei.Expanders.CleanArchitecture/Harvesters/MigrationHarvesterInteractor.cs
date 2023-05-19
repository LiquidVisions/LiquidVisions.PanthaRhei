using System.Collections.Generic;
using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Harvesters
{
    /// <summary>
    /// A <seealso cref="IHarvesterInteractor{TExpander}"/> responsible for harvesting the migration files.
    /// </summary>
    public class MigrationHarvesterInteractor : IHarvesterInteractor<CleanArchitectureExpander>
    {
        private readonly string migrationsFolder;
        private readonly ICreateGateway<Harvest> gateway;
        private readonly GenerationOptions options;
        private readonly CleanArchitectureExpander expander;
        private readonly IFile file;
        private readonly IDirectory directory;

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationHarvesterInteractor"/> class.
        /// </summary>
        /// <param name="factory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public MigrationHarvesterInteractor(IDependencyFactoryInteractor factory)
        {
            gateway = factory.Get<ICreateGateway<Harvest>>();
            options = factory.Get<GenerationOptions>();
            expander = factory.Get<CleanArchitectureExpander>();
            file = factory.Get<IFile>();
            directory = factory.Get<IDirectory>();

            migrationsFolder = System.IO.Path.Combine(options.OutputFolder, Resources.InfrastructureMigrationsFolder);
        }

        /// <inheritdoc/>
        public bool CanExecute => options.Modes.HasFlag(GenerationModes.Harvest)
            && directory.Exists(migrationsFolder);

        public CleanArchitectureExpander Expander => expander;

        /// <inheritdoc/>
        public void Execute()
        {
            directory.GetFiles(migrationsFolder, "*.cs", SearchOption.AllDirectories)
                .ToList()
                .ForEach(x => HarvestSingle(x));
        }

        private void HarvestSingle(string fullPathToSourceFile)
        {
            Harvest harvest = new(Resources.MigrationHarvesterExtensionFile)
            {
                Path = fullPathToSourceFile,
                Items = new List<HarvestItem>
                {
                    new HarvestItem { Content = file.ReadAllText(fullPathToSourceFile) },
                },
            };

            gateway.Create(harvest);
        }
    }
}
