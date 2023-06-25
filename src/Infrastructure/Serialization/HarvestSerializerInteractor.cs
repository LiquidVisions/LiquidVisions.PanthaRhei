using System.Linq;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Domain.IO;

namespace LiquidVisions.PanthaRhei.Infrastructure.Serialization
{
    public class HarvestSerializerInteractor : IHarvestSerializerInteractor
    {
        private readonly ISerializerInteractor<Harvest> serializer;
        private readonly IFile file;
        private readonly IDirectory directory;

        public HarvestSerializerInteractor(IDependencyFactory dependencyFactory)
        {
            file = dependencyFactory.Get<IFile>();
            directory = dependencyFactory.Get<IDirectory>();
            serializer = dependencyFactory.Get<ISerializerInteractor<Harvest>>();
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
