using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Rejuvenator;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Rejuvenator
{
    /// <summary>
    /// A <seealso cref="Rejuvenator{TExpander}"/> that handles EntityFramework migrations.
    /// </summary>
    public class MigrationRejuvenator : Rejuvenator<CleanArchitectureExpander>
    {
        private readonly IDirectory directoryService;
        private readonly IGetRepository<Harvest> getGateway;
        private readonly IFile fileService;
        private readonly string folder;

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationRejuvenator"/> class.
        /// </summary>
        /// <param name="factory"><seealso cref="IDependencyFactory"/></param>
        public MigrationRejuvenator(IDependencyFactory factory)
            : base(factory)
        {
            GenerationOptions options = factory.Resolve<GenerationOptions>();

            directoryService = factory.Resolve<IDirectory>();
            getGateway = factory.Resolve<IGetRepository<Harvest>>();
            fileService = factory.Resolve<IFile>();
            folder = Path.Combine(options.HarvestFolder, Expander.Model.Name);
        }

        /// <summary>
        /// Gets a value indicating whether the <seealso cref="Rejuvenator{TExpander}"/> can be executed.
        /// Prerequisit is that the harvest folder should exist.
        /// </summary>
        public override bool Enabled => directoryService.Exists(folder);

        /// <inheritdoc/>
        protected override string Extension => Resources.MigrationHarvesterExtensionFile;

        /// <inheritdoc/>
        public override void Execute()
        {
            string[] files = directoryService.GetFiles(folder, $"*.{Extension}", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                Harvest harvest = getGateway.GetById(file);
                var item = harvest.Items.Single();
                string content = item.Content;

                directoryService.Create(fileService.GetDirectory(harvest.Path));

                fileService.WriteAllText(harvest.Path, content);
            }
        }
    }
}
