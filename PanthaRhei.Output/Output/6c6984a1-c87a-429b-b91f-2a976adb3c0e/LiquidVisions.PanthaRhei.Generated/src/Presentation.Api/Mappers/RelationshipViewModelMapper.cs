using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using System.Linq;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers
{
    public class RelationshipViewModelMapper : IMapper<Relationship, RelationshipViewModel>
    {
        public void Map(Relationship source, RelationshipViewModel target)
        {
            target.Id = source.Id;
            target.Key = source.Key != null ? new FieldViewModelMapper().Map(source.Key) : null;
            target.Entity = source.Entity != null ? new EntityViewModelMapper().Map(source.Entity) : null;
            target.Cardinality = source.Cardinality;
            target.WithForeignEntityKey = source.WithForeignEntityKey != null ? new FieldViewModelMapper().Map(source.WithForeignEntityKey) : null;
            target.WithForeignEntity = source.WithForeignEntity != null ? new EntityViewModelMapper().Map(source.WithForeignEntity) : null;
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
