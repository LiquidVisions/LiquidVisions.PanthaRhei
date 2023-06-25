using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Gateways;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Rejuvenator;
using LiquidVisions.PanthaRhei.Domain.IO;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Rejuvenator
{
    /// <summary>
    /// A <seealso cref="RejuvenatorInteractor{TExpander}"/> that handles EntityFramework migrations.
    /// </summary>
    public class MigrationRejuvenator : RejuvenatorInteractor<CleanArchitectureExpander>
    {
        private readonly IDirectory directoryService;
        private readonly IGetGateway<Harvest> getGateway;
        private readonly IFile fileService;
        private readonly string folder;

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationRejuvenator"/> class.
        /// </summary>
        /// <param name="factory"><seealso cref="IDependencyFactory"/></param>
        public MigrationRejuvenator(IDependencyFactory factory)
            : base(factory)
        {
            GenerationOptions options = factory.Get<GenerationOptions>();

            directoryService = factory.Get<IDirectory>();
            getGateway = factory.Get<IGetGateway<Harvest>>();
            fileService = factory.Get<IFile>();
            folder = Path.Combine(options.HarvestFolder, Expander.Model.Name);
        }

        /// <summary>
        /// Gets a value indicating whether the <seealso cref="RejuvenatorInteractor{TExpander}"/> can be executed.
        /// Prerequisit is that the harvest folder should exist.
        /// </summary>
        public override bool CanExecute => directoryService.Exists(folder);

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
