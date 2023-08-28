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
        private readonly ISerializer<Harvest> _serializer;
        private readonly IFile _file;
        private readonly IDirectory _directory;

        /// <summary>
        /// Initializes a new instance of the <see cref="HarvestSerializer"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public HarvestSerializer(IDependencyFactory dependencyFactory)
        {
            _file = dependencyFactory.Get<IFile>();
            _directory = dependencyFactory.Get<IDirectory>();
            _serializer = dependencyFactory.Get<ISerializer<Harvest>>();
        }

        /// <inheritdoc/>
        public void Serialize(Harvest harvest, string fullPath)
        {
            bool serialize = _file.Exists(fullPath);
            serialize &= harvest.Items.Any();
            if (serialize)
            {
                string dir = _file.GetDirectory(fullPath);
                if (!_directory.Exists(dir))
                {
                    _directory.Create(dir);
                }

                _serializer.Serialize(fullPath, harvest);
            }
        }
    }
}
