using LiquidVisions.PanthaRhei.Generated.Client.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using System.Linq;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers
{
    public class RelationshipModelMapper : IMapper<Relationship, RelationshipViewModel>
    {
        public void Map(Relationship source, RelationshipViewModel target)
        {
            target.Id = source.Id;
            target.Key = new FieldModelMapper().Map(source.Key);
            target.Entity = new EntityModelMapper().Map(source.Entity);
            target.Cardinality = source.Cardinality;
            target.WithForeignEntityKey = new FieldModelMapper().Map(source.WithForeignEntityKey);
            target.WithForeignEntity = new EntityModelMapper().Map(source.WithForeignEntity);
            target.WithCardinality = source.WithCardinality;
            target.Required = source.Required;
        }

        public RelationshipViewModel Map(Relationship source)
        {
			RelationshipViewModel target = new();

            Map(source, target);

            return target;
        }
    }
}
