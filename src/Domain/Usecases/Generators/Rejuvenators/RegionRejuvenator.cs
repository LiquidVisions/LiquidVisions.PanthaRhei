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
        private readonly IDirectory _directoryService;
        private readonly IGetRepository<Harvest> _harvestGateway;
        private readonly IWriter _writer;
        private readonly string _folder;
        private readonly GenerationOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionRejuvenator{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public RegionRejuvenator(IDependencyFactory dependencyFactory)
            : base(dependencyFactory)
        {
            _options = dependencyFactory.Resolve<GenerationOptions>();

            _directoryService = dependencyFactory.Resolve<IDirectory>();
            _harvestGateway = dependencyFactory.Resolve<IGetRepository<Harvest>>();
            _writer = dependencyFactory.Resolve<IWriter>();
            _folder = Path.Combine(_options.HarvestFolder, App.FullName);
        }

        /// <inheritdoc/>
        public override bool Enabled => _options.Modes.HasFlag(GenerationModes.Rejuvenate)
            && _directoryService.Exists(_folder);

        /// <inheritdoc/>
        protected override string Extension => Resources.RegionHarvesterExtensionFile;

        /// <inheritdoc/>
        public override void Execute()
        {
            string[] files = _directoryService.GetFiles(_folder, $"*.{Extension}", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                Harvest harvest = _harvestGateway.GetById(file);
                HandleReplace(harvest);

                _writer.Save(harvest.Path);
            }
        }

        private void HandleReplace(Harvest harvest)
        {
            _writer.Load(harvest.Path);

            foreach (HarvestItem item in harvest.Items)
            {
                string tag = item.Tag.Trim().ReplaceLineEndings();

                string begin = $"#region ns-custom-{tag}";
                string end = $"#endregion ns-custom-{tag}";
                string content = item.Content.Trim().ReplaceLineEndings();

                _writer.AddBetween(begin, end, content);
            }
        }
    }
}
