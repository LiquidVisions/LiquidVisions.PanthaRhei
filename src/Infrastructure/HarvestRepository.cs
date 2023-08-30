using System;
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
    /// <summary>
    /// Repository for <see cref="Harvest"/> entities.
    /// </summary>
    internal class HarvestRepository : ICreateRepository<Harvest>, IGetRepository<Harvest>
    {
        private readonly IHarvestSerializer _serializer;
        private readonly GenerationOptions _expandRequestModel;
        private readonly App _app;
        private readonly IDeserializer<Harvest> _deserializer;
        private readonly IFile _file;

        /// <summary>
        /// Initializes a new instance of the <see cref="HarvestRepository"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        public HarvestRepository(IDependencyFactory dependencyFactory)
        {
            _file = dependencyFactory.Resolve<IFile>();
            _deserializer = dependencyFactory.Resolve<IDeserializer<Harvest>>();
            _serializer = dependencyFactory.Resolve<IHarvestSerializer>();
            _expandRequestModel = dependencyFactory.Resolve<GenerationOptions>();
            _app = dependencyFactory.Resolve<App>();
        }

        /// <inheritdoc/>
        public bool Create(Harvest entity)
        {
            if (string.IsNullOrEmpty(entity.HarvestType))
            {
                throw new InvalidProgramException("Expected harvest type.");
            }

            string fullPath = Path.Combine(
                _expandRequestModel.HarvestFolder,
                _app.FullName,
                $"{_file.GetFileNameWithoutExtension(entity.Path)}.{entity.HarvestType}");

            _serializer.Serialize(entity, fullPath);

            return true;
        }

        /// <inheritdoc/>
        public IEnumerable<Harvest> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Harvest GetById(object id)
        {
            string path = (string)id;
            if (!_file.Exists(path))
            {
                throw new FileNotFoundException($"Harvest file not found on path {path}");
            }

            return _deserializer.Deserialize(path);
        }
    }
}
