using System;
using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Gateways;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Infrastructure.Serialization;

namespace LiquidVisions.PanthaRhei.Infrastructure
{
    internal class HarvestRepository : ICreateGateway<Harvest>, IGetGateway<Harvest>
    {
        private readonly IHarvestSerializerInteractor serializer;
        private readonly GenerationOptions expandRequestModel;
        private readonly App app;
        private readonly IDeserializerInteractor<Harvest> deserializer;
        private readonly IFile file;

        public HarvestRepository(IDependencyFactoryInteractor dependencyFactory)
        {
            file = dependencyFactory.Get<IFile>();
            deserializer = dependencyFactory.Get<IDeserializerInteractor<Harvest>>();
            serializer = dependencyFactory.Get<IHarvestSerializerInteractor>();
            expandRequestModel = dependencyFactory.Get<GenerationOptions>();
            app = dependencyFactory.Get<App>();
        }

        public bool Create(Harvest entity)
        {
            if (string.IsNullOrEmpty(entity.HarvestType))
            {
                throw new InvalidProgramException("Expected harvest type.");
            }

            string fullPath = Path.Combine(
                expandRequestModel.HarvestFolder,
                app.FullName,
                $"{file.GetFileNameWithoutExtension(entity.Path)}.{entity.HarvestType}");

            serializer.Serialize(entity, fullPath);

            return true;
        }

        public IEnumerable<Harvest> GetAll()
        {
            throw new NotImplementedException();
        }

        public Harvest GetById(object id)
        {
            string path = (string)id;
            if (!file.Exists(path))
            {
                throw new FileNotFoundException($"Harvest file not found on path {path}");
            }

            return deserializer.Deserialize(path);
        }
    }
}
