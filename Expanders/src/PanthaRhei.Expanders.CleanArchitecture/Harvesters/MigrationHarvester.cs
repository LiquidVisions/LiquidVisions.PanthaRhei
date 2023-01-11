using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Harvesters;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Harvesters
{
    /// <summary>
    /// A <seealso cref="HarvesterInteractor{TExpander}"/> responsible for harvesting the migration files.
    /// </summary>
    public class MigrationHarvester : HarvesterInteractor<CleanArchitectureExpander>
    {
        private readonly string migrationsFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationHarvester"/> class.
        /// </summary>
        /// <param name="factory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public MigrationHarvester(IDependencyFactoryInteractor factory)
            : base(factory)
        {
            migrationsFolder = Path.Combine(Parameters.OutputFolder, Resources.InfrastructureMigrationsFolder);
        }

        /// <inheritdoc/>
        public override bool CanExecute => Directory.Exists(migrationsFolder);

        /// <inheritdoc/>
        protected override string Extension => Resources.MigrationHarvesterExtensionFile;

        /// <inheritdoc/>
        public override void Execute()
        {
            string[] files = Directory.GetFiles(migrationsFolder, "*.cs", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                HarvestSingle(file);
            }
        }

        private void HarvestSingle(string file)
        {
            Harvest harvest = new()
            {
                Path = file,
                Items = new List<HarvestItem>
                {
                    new HarvestItem { Content = File.ReadAllText(file) },
                },
            };

            DeserializeHarvestModelToFile(harvest, file);
        }
    }
}
