﻿using System;
using System.Collections.Generic;
using System.IO;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Infrastructure.Serialization;

namespace LiquidVisions.PanthaRhei.Infrastructure
{
    internal class HarvestRepository : ICreateRepository<Harvest>, IGetRepository<Harvest>
    {
        private readonly IHarvestSerializer serializer;
        private readonly GenerationOptions expandRequestModel;
        private readonly App app;
        private readonly IDeserializer<Harvest> deserializer;
        private readonly IFile file;

        public HarvestRepository(IDependencyFactory dependencyFactory)
        {
            file = dependencyFactory.Get<IFile>();
            deserializer = dependencyFactory.Get<IDeserializer<Harvest>>();
            serializer = dependencyFactory.Get<IHarvestSerializer>();
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