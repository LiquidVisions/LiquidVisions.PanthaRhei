using System;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;

namespace LiquidVisions.PanthaRhei.Infrastructure.Serialization
{
    /// <summary>
    /// Represents a Harvest Serializer.
    /// </summary>
    public class HarvestSerializer : IHarvestSerializer
    {
        private readonly ISerializer<Harvest> serializer;
        private readonly IFile file;
        private readonly IDirectory directory;

        /// <summary>
        /// Initializes a new instance of the <see cref="HarvestSerializer"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public HarvestSerializer(IDependencyFactory dependencyFactory)
        {
            ArgumentNullException.ThrowIfNull(dependencyFactory);

            file = dependencyFactory.Resolve<IFile>();
            directory = dependencyFactory.Resolve<IDirectory>();
            serializer = dependencyFactory.Resolve<ISerializer<Harvest>>();
        }

        /// <inheritdoc/>
        public void Serialize(Harvest harvest, string fullPath)
        {
            ArgumentNullException.ThrowIfNull(harvest);

            bool serialize = file.Exists(fullPath);
            serialize &= harvest.Items.Count != 0;
            if (serialize)
            {
                string dir = file.GetDirectory(fullPath);
                if (!directory.Exists(dir))
                {
                    directory.Create(dir);
                }

                serializer.Serialize(fullPath, harvest);
            }
        }
    }
}
