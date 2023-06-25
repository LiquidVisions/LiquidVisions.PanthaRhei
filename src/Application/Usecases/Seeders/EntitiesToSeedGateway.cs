using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Repositories;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Seeders
{
    internal class EntitiesToSeedGateway : IEntitiesToSeedRepository
    {
        [ExcludeFromCodeCoverage]
        public IEnumerable<Type> GetAll()
        {
            Type type = typeof(Entity);

            return type.Assembly
                .GetTypes()
                .Where(x => x.IsClass)
                .Where(x => x.Namespace == type.Namespace);
        }

        public Type GetById(object id)
        {
            throw new NotImplementedException();
        }
    }
}
