using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Mappers.Relationships
{
    internal class CreateRelationshipRequestModelMapper : IMapper<CreateRelationshipRequestModel, Relationship>
    {
        public void Map(CreateRelationshipRequestModel source, Relationship target)
        {
            target.Key = source.Key;
            target.Entity = source.Entity;
            target.Cardinality = source.Cardinality;
            target.WithForeignEntityKey = source.WithForeignEntityKey;
            target.WithForeignEntity = source.WithForeignEntity;
            target.WithCardinality = source.WithCardinality;
            target.Required = source.Required;
        }

        public Relationship Map(CreateRelationshipRequestModel source)
        {
            Relationship target = new();

            Map(source, target);

            return target;
        }
    }
}
