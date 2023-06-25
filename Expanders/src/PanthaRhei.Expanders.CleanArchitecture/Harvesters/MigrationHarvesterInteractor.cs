using System.Collections.Generic;
using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Harvesters
{
    /// <summary>
    /// A <seealso cref="IHarvester{TExpander}"/> responsible for harvesting the migration files.
    /// </summary>
    public class MigrationHarvesterInteractor : IHarvester<CleanArchitectureExpander>
    {
        private readonly string migrationsFolder;
        private readonly ICreateRepository<Harvest> gateway;
        private readonly GenerationOptions options;
        private readonly CleanArchitectureExpander expander;
        private readonly IFile file;
        private readonly IDirectory directory;

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationHarvesterInteractor"/> class.
        /// </summary>
        /// <param name="factory"><seealso cref="IDependencyFactory"/></param>
        public MigrationHarvesterInteractor(IDependencyFactory factory)
        {
            gateway = factory.Get<ICreateRepository<Harvest>>();
            options = factory.Get<GenerationOptions>();
            expander = factory.Get<CleanArchitectureExpander>();
            file = factory.Get<IFile>();
            directory = factory.Get<IDirectory>();

            migrationsFolder = System.IO.Path.Combine(options.OutputFolder, Resources.InfrastructureMigrationsFolder);
        }

        /// <inheritdoc/>
        public bool Enabled => options.Modes.HasFlag(GenerationModes.Harvest)
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
