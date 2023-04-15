using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Relationships;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Mappers.Relationships
{
    internal class UpdateRelationshipRequestModelRequestModelMapper : IMapper<UpdateRelationshipRequestModel, Relationship>
    {
        public void Map(UpdateRelationshipRequestModel source, Relationship target)
        {
            target.Key = source.Key;
            target.Entity = source.Entity;
            target.Cardinality = source.Cardinality;
            target.WithForeignEntityKey = source.WithForeignEntityKey;
            target.WithForeignEntity = source.WithForeignEntity;
            target.WithCardinality = source.WithCardinality;
            target.Required = source.Required;
        }

        public Relationship Map(UpdateRelationshipRequestModel source)
        {
            Relationship target = new();

            Map(source, target);

            return target;
        }
    }
}
