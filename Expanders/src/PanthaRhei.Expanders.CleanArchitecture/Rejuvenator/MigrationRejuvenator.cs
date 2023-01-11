﻿using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Rejuvenator;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Serialization;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture.Rejuvenator
{
    /// <summary>
    /// A <seealso cref="RejuvenatorInteractor{TExpander}"/> that handles EntityFramework migrations.
    /// </summary>
    public class MigrationRejuvenator : RejuvenatorInteractor<CleanArchitectureExpander>
    {
        private readonly IDirectory directoryService;
        private readonly IDeserializerInteractor<Harvest> deserializer;
        private readonly IFile fileService;
        private readonly string folder;

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationRejuvenator"/> class.
        /// </summary>
        /// <param name="factory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public MigrationRejuvenator(IDependencyFactoryInteractor factory)
            : base(factory)
        {
            Parameters parameters = factory.Get<Parameters>();

            directoryService = factory.Get<IDirectory>();
            deserializer = factory.Get<IDeserializerInteractor<Harvest>>();
            fileService = factory.Get<IFile>();
            folder = Path.Combine(parameters.HarvestFolder, Expander.Model.Name);
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
                Harvest harvest = deserializer.Deserialize(file);
                var item = harvest.Items.Single();
                string content = item.Content;

                directoryService.Create(fileService.GetDirectory(harvest.Path));

                fileService.WriteAllText(harvest.Path, content);
            }
        }
    }
}