using System;
using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Infrastructure.Serialization;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure
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
            if(string.IsNullOrEmpty(entity.HarvestType))
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
            if(!file.Exists(path))
            {
                throw new FileNotFoundException($"Harvest file not found on path {path}");
            }

            return deserializer.Deserialize(path);
        }
    }
}
