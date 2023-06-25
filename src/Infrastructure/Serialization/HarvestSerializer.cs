using System.Linq;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;

namespace LiquidVisions.PanthaRhei.Infrastructure.Serialization
{
    public class HarvestSerializer : IHarvestSerializer
    {
        private readonly ISerializer<Harvest> serializer;
        private readonly IFile file;
        private readonly IDirectory directory;

        public HarvestSerializer(IDependencyFactory dependencyFactory)
        {
            file = dependencyFactory.Get<IFile>();
            directory = dependencyFactory.Get<IDirectory>();
            serializer = dependencyFactory.Get<ISerializer<Harvest>>();
        }

        public void Serialize(Harvest harvest, string fullPath)
        {
            bool serialize = file.Exists(fullPath);
            serialize &= harvest.Items.Any();
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
