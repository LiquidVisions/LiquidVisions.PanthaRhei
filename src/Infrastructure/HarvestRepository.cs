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
    /// <remarks>
    /// Initializes a new instance of the <see cref="HarvestRepository"/> class.
    /// </remarks>
    /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
    internal class HarvestRepository(IDependencyFactory dependencyFactory) : ICreateRepository<Harvest>, IGetRepository<Harvest>
    {
        private readonly IHarvestSerializer _serializer = dependencyFactory.Resolve<IHarvestSerializer>();
        private readonly GenerationOptions _expandRequestModel = dependencyFactory.Resolve<GenerationOptions>();
        private readonly App _app = dependencyFactory.Resolve<App>();
        private readonly IDeserializer<Harvest> _deserializer = dependencyFactory.Resolve<IDeserializer<Harvest>>();
        private readonly IFile _file = dependencyFactory.Resolve<IFile>();

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
