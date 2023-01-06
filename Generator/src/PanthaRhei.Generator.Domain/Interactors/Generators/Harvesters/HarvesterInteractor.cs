using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Serialization;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Harvesters
{
    /// <summary>
    /// An abstract implementation of the <see cref="IHarvesterInteractor{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpanderInteractor"/>.</typeparam>
    public abstract class HarvesterInteractor<TExpander> : IHarvesterInteractor<TExpander>
        where TExpander : class, IExpanderInteractor
    {
        private readonly IFile file;
        private readonly IDirectory directory;
        private readonly ISerializerInteractor<Harvest> serializer;
        private readonly TExpander expander;
        private readonly Parameters parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="HarvesterInteractor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        protected HarvesterInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            file = dependencyFactory.Get<IFile>();
            directory = dependencyFactory.Get<IDirectory>();
            serializer = dependencyFactory.Get<ISerializerInteractor<Harvest>>();
            expander = dependencyFactory.Get<TExpander>();
            parameters = dependencyFactory.Get<Parameters>();
        }

        /// <inheritdoc/>
        public abstract bool CanExecute { get; }

        /// <inheritdoc/>
        public TExpander Expander => expander;

        /// <summary>
        /// Gets the <seealso cref="IFile"/>.
        /// </summary>
        protected IFile File => file;

        /// <summary>
        /// Gets the <seealso cref="IDirectory"/>.
        /// </summary>
        protected IDirectory Directory => directory;

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
        internal virtual void DeserializeHarvestModelToFile(Harvest harvest, string sourceFile)
        {
            string fullPath = System.IO.Path.Combine(parameters.HarvestFolder, Expander.Model.Name, $"{file.GetFileNameWithoutExtension(sourceFile)}.{Extension}");

            bool serialize = File.Exists(fullPath) && !harvest.Items.Any();
            serialize |= harvest.Items.Any();
            if (serialize)
            {
                string dir = file.GetDirectory(fullPath);
                if (!this.directory.Exists(dir))
                {
                    this.directory.Create(dir);
                }

                serializer.Serialize(fullPath, harvest);
            }
        }
    }
}
