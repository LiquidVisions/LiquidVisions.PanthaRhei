using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Serialization;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Harvesters
{
    public class HarvestSerializerInteractor : IHarvestSerializerInteractor
    {
        private readonly ISerializerInteractor<Harvest> serializer;
        private readonly IFile file;
        private readonly IDirectory directory;

        public HarvestSerializerInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            file = dependencyFactory.Get<IFile>();
            directory = dependencyFactory.Get<IDirectory>();
            serializer = dependencyFactory.Get<ISerializerInteractor<Harvest>>();
        }

        public void Deserialize(Harvest harvest, string fullPath)
        {
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
