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
            target.Key = new FieldViewModelMapper().Map(source.Key);
            target.Entity = new EntityViewModelMapper().Map(source.Entity);
            target.Cardinality = source.Cardinality;
            target.WithForeignEntityKey = new FieldViewModelMapper().Map(source.WithForeignEntityKey);
            target.WithForeignEntity = new EntityViewModelMapper().Map(source.WithForeignEntity);
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
