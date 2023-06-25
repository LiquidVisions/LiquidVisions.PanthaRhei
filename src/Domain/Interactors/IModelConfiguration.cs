using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Models;

namespace LiquidVisions.PanthaRhei.Domain.Interactors
{
    public interface IModelConfiguration
    {
        string[] GetIndexes(Type entityType);

        string[] GetKeys(Type entityType);

        int? GetSize(Type entityType, string propName);

        bool GetIsRequired(Type entityType, string propName);

        List<RelationshipDto> GetRelationshipInfo(Entity entity);
    }
}
