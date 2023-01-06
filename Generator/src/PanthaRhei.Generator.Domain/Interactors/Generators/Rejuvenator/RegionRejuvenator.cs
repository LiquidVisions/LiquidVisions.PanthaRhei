﻿using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Serialization;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Rejuvenator
{
    /// <summary>
    /// An abstract implementation of the <see cref="IRejuvenator{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpander"/>.</typeparam>
    public sealed class RegionRejuvenator<TExpander> : Rejuvenator<TExpander>
        where TExpander : class, IExpander
    {
        private readonly IDirectory directoryService;
        private readonly IDeserializer<Harvest> deserializer;
        private readonly IWriter writer;
        private readonly string folder;
        private readonly Parameters parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionRejuvenator{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyResolver"><seealso cref="IDependencyResolver"/></param>
        public RegionRejuvenator(IDependencyResolver dependencyResolver)
            : base(dependencyResolver)
        {
            parameters = dependencyResolver.Get<Parameters>();

            directoryService = dependencyResolver.Get<IDirectory>();
            deserializer = dependencyResolver.Get<IDeserializer<Harvest>>();
            writer = dependencyResolver.Get<IWriter>();
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