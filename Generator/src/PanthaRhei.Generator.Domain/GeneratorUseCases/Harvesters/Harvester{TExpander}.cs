using System.Linq;
using System.Runtime.CompilerServices;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Serialization;

namespace LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Harvesters
{
    /// <summary>
    /// An abstract implementation of the <see cref="IHarvester{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpander"/>.</typeparam>
    public abstract class Harvester<TExpander> : IHarvester<TExpander>
        where TExpander : class, IExpander
    {
        private readonly IFileService fileService;
        private readonly IDirectoryService directoryService;
        private readonly ISerializer<Harvest> serializer;
        private readonly TExpander expander;
        private readonly Parameters parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="Harvester{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyResolver"><seealso cref="IDependencyResolver"/></param>
        protected Harvester(IDependencyResolver dependencyResolver)
        {
            fileService = dependencyResolver.Get<IFileService>();
            directoryService = dependencyResolver.Get<IDirectoryService>();
            serializer = dependencyResolver.Get<ISerializer<Harvest>>();
            expander = dependencyResolver.Get<TExpander>();
            parameters = dependencyResolver.Get<Parameters>();
        }

        /// <inheritdoc/>
        public abstract bool CanExecute { get; }

        /// <inheritdoc/>
        public TExpander Expander => expander;

        /// <summary>
        /// Gets the <seealso cref="IFileService"/>.
        /// </summary>
        protected IFileService FileService => fileService;

        /// <summary>
        /// Gets the <seealso cref="IDirectoryService"/>.
        /// </summary>
        protected IDirectoryService DirectoryService => directoryService;

        /// <summary>
        /// Gets the extension of the harvest file.
        /// </summary>
        protected abstract string Extension { get; }

        /// <inheritdoc/>
        public abstract void Execute();

        /// <summary>
        /// Serializes the <seealso cref="Harvest"/> file and saves it in the location HarvestFolder.ExpanderName.SourceFileName.harvest.
        /// Deserialisation is only done when the <seealso cref="Harvest.Items"/> has valid <seealso cref="HarvestItem">Harvest items</seealso>.
        /// </summary>
        /// <param name="harvest"><seealso cref="Harvest"/></param>
        /// <param name="sourceFile">The full path to the source location.</param>
        protected virtual void DeserializeHarvestModelToFile(Harvest harvest, string sourceFile)
        {
            string fullPath = System.IO.Path.Combine(parameters.HarvestFolder, Expander.Model.Name, $"{fileService.GetFileNameWithoutExtension(sourceFile)}.{Extension}");
            if (FileService.Exists(fullPath) && !harvest.Items.Any() || harvest.Items.Any())
            {
                string directory = fileService.GetDirectory(fullPath);
                if (!directoryService.Exists(directory))
                {
                    directoryService.Create(directory);
                }

                serializer.Serialize(fullPath, harvest);
            }
        }
    }
}
