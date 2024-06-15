using System.IO;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Rejuvenators
{
    /// <summary>
    /// An abstract implementation of the <see cref="IRejuvenator{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A derived type of <see cref="IExpander"/>.</typeparam>
    internal sealed class RegionRejuvenator<TExpander> : Rejuvenator<TExpander>
        where TExpander : class, IExpander
    {
        private readonly IDirectory directoryService;
        private readonly IGetRepository<Harvest> harvestGateway;
        private readonly IWriter writer;
        private readonly string folder;
        private readonly GenerationOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionRejuvenator{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public RegionRejuvenator(IDependencyFactory dependencyFactory)
            : base(dependencyFactory)
        {
            options = dependencyFactory.Resolve<GenerationOptions>();

            directoryService = dependencyFactory.Resolve<IDirectory>();
            harvestGateway = dependencyFactory.Resolve<IGetRepository<Harvest>>();
            writer = dependencyFactory.Resolve<IWriter>();
            folder = Path.Combine(options.HarvestFolder, App.FullName, string.Join('.', Expander.Name.Split('.')[1..^0]));
        }

        /// <inheritdoc/>
        public override bool Enabled => options.Modes.HasFlag(GenerationModes.Rejuvenate)
            && directoryService.Exists(folder);

        /// <inheritdoc/>
        protected override string Extension => Resources.RegionHarvesterExtensionFile;

        /// <inheritdoc/>
        public override void Execute()
        {
            string[] files = directoryService.GetFiles(folder, $"*{Extension}", SearchOption.AllDirectories);
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

            foreach (HarvestItem item in harvest.Items)
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
