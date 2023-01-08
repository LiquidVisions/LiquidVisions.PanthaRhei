using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors
{
    public interface IModelConfiguration
    {
        string[] GetIndexes(Type entityType);

        string[] GetKeys(Type entityType);
        List<Dictionary<string, string>> GetRelationshipInfo(Entity entity);
    }
}
