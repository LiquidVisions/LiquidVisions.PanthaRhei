using System.Collections.Generic;
using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain;
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
        private readonly IHarvestSerializerInteractor serializer;
        private readonly Parameters parameters;
        private readonly CleanArchitectureExpander expander;
        private readonly IFile file;
        private readonly IDirectory directory;

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationHarvesterInteractor"/> class.
        /// </summary>
        /// <param name="factory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public MigrationHarvesterInteractor(IDependencyFactoryInteractor factory)
        {
            serializer = factory.Get<IHarvestSerializerInteractor>();
            parameters = factory.Get<Parameters>();
            expander = factory.Get<CleanArchitectureExpander>();
            file = factory.Get<IFile>();
            directory = factory.Get<IDirectory>();

            migrationsFolder = System.IO.Path.Combine(parameters.OutputFolder, Resources.InfrastructureMigrationsFolder);
        }

        /// <inheritdoc/>
        public bool CanExecute => directory.Exists(migrationsFolder);

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
            Harvest harvest = new()
            {
                Path = fullPathToSourceFile,
                Items = new List<HarvestItem>
                {
                    new HarvestItem { Content = file.ReadAllText(fullPathToSourceFile) },
                },
            };

            string fullPath = Path.Combine(
                parameters.HarvestFolder,
                expander.Model.Name,
                $"{file.GetFileNameWithoutExtension(fullPathToSourceFile)}.{Resources.MigrationHarvesterExtensionFile}");

            serializer.Deserialize(harvest, fullPath);
        }
    }
}
