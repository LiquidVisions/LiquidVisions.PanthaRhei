using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Serialization;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Rejuvenator
{
    /// <summary>
    /// An abstract implementation of the <see cref="IRejuvenatorInteractor{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpanderInteractor"/>.</typeparam>
    internal sealed class RegionRejuvenatorInteractor<TExpander> : RejuvenatorInteractor<TExpander>
        where TExpander : class, IExpanderInteractor
    {
        private readonly IDirectory directoryService;
        private readonly IDeserializerInteractor<Harvest> deserializer;
        private readonly IWriter writer;
        private readonly string folder;
        private readonly Parameters parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionRejuvenatorInteractor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public RegionRejuvenatorInteractor(IDependencyFactoryInteractor dependencyFactory)
            : base(dependencyFactory)
        {
            parameters = dependencyFactory.Get<Parameters>();

            directoryService = dependencyFactory.Get<IDirectory>();
            deserializer = dependencyFactory.Get<IDeserializerInteractor<Harvest>>();
            writer = dependencyFactory.Get<IWriter>();
            folder = Path.Combine(parameters.HarvestFolder, Expander.Model.Name);
        }

        /// <inheritdoc/>
        public override bool CanExecute => !parameters.GenerationMode.HasFlag(GenerationModes.Deploy) && directoryService.Exists(folder);

        /// <inheritdoc/>
        protected override string Extension => Resources.RegionHarvesterExtensionFile;

        /// <inheritdoc/>
        public override void Execute()
        {
            string[] files = directoryService.GetFiles(folder, $"*.{Extension}", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                Harvest harvest = deserializer.Deserialize(file);
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
