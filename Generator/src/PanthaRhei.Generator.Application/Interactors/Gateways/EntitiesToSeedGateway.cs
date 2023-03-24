using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Gateways
{
    internal class EntitiesToSeedGateway : IEntitiesToSeedGateway
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
