using System.IO;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Gateways;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Domain.IO;

namespace LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Rejuvenator
{
    /// <summary>
    /// An abstract implementation of the <see cref="IRejuvenatorInteractor{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpanderInteractor"/>.</typeparam>
    internal sealed class RegionRejuvenatorInteractor<TExpander> : RejuvenatorInteractor<TExpander>
        where TExpander : class, IExpanderInteractor
    {
        private readonly IDirectory directoryService;
        private readonly IGetGateway<Harvest> harvestGateway;
        private readonly IWriterInteractor writer;
        private readonly string folder;
        private readonly GenerationOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionRejuvenatorInteractor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public RegionRejuvenatorInteractor(IDependencyFactoryInteractor dependencyFactory)
            : base(dependencyFactory)
        {
            options = dependencyFactory.Get<GenerationOptions>();

            directoryService = dependencyFactory.Get<IDirectory>();
            harvestGateway = dependencyFactory.Get<IGetGateway<Harvest>>();
            writer = dependencyFactory.Get<IWriterInteractor>();
            folder = Path.Combine(options.HarvestFolder, App.FullName);
        }

        /// <inheritdoc/>
        public override bool CanExecute => options.Modes.HasFlag(GenerationModes.Rejuvenate)
            && directoryService.Exists(folder);

        /// <inheritdoc/>
        protected override string Extension => Resources.RegionHarvesterExtensionFile;

        /// <inheritdoc/>
        public override void Execute()
        {
            string[] files = directoryService.GetFiles(folder, $"*.{Extension}", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                Harvest harvest = harvestGateway.GetById(file);
                HandleReplace(harvest);

                writer.Save(harvest.Path);
            }
        }

        private void HandleReplace(Harvest harvest)
        {
            writer.Load(harvest.Path);

            foreach (var item in harvest.Items)
            {
                string tag = item.Tag.Trim().ReplaceLineEndings();

                string begin = $"#region ns-custom-{tag}";
                string end = $"#endregion ns-custom-{tag}";
                string content = item.Content.Trim().ReplaceLineEndings();

                writer.AddBetween(begin, end, content);
            }
        }
    }
}
