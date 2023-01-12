using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Gateways
{
    internal class EntitiesToSeedGateway : IEntitiesToSeedGateway
    {
        public IEnumerable<Type> GetAll()
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.Namespace == typeof(Entity).Namespace);
        }

        public Type GetById(object id)
        {
            throw new NotImplementedException();
        }
    }
}
