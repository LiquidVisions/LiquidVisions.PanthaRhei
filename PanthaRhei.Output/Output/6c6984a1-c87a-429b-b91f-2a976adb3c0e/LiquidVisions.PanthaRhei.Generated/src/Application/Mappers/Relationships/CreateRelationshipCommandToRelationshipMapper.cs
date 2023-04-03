using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Mappers.Relationships
{
    internal class CreateRelationshipCommandToRelationshipMapper : IMapper<CreateRelationshipCommand, Relationship>
    {
        public void Map(CreateRelationshipCommand source, Relationship target)
        {
            target.Key = source.Key;
            target.Entity = source.Entity;
            target.Cardinality = source.Cardinality;
            target.WithForeignEntityKey = source.WithForeignEntityKey;
            target.WithForeignEntity = source.WithForeignEntity;
            target.WithCardinality = source.WithCardinality;
            target.Required = source.Required;
        }

        public Relationship Map(CreateRelationshipCommand source)
        {
            Relationship target = new();

            Map(source, target);

            return target;
        }
    }
}
