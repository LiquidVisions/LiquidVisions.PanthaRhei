﻿using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Relationships;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Mappers.Relationships
{
    internal class UpdateRelationshipCommandToRelationshipMapper : IMapper<UpdateRelationshipCommand, Relationship>
    {
        public void Map(UpdateRelationshipCommand source, Relationship target)
        {
            target.Key = source.Key;
            target.Entity = source.Entity;
            target.Cardinality = source.Cardinality;
            target.WithForeignEntityKey = source.WithForeignEntityKey;
            target.WithForeignEntity = source.WithForeignEntity;
            target.WithCardinality = source.WithCardinality;
            target.Required = source.Required;
        }

        public Relationship Map(UpdateRelationshipCommand source)
        {
            Relationship target = new();

            Map(source, target);

            return target;
        }
    }
}
