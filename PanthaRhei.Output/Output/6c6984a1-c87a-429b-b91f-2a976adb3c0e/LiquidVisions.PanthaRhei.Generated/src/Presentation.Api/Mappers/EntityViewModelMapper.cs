using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using System.Linq;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers
{
    public class EntityViewModelMapper : IMapper<Entity, EntityViewModel>
    {
        public void Map(Entity source, EntityViewModel target)
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.Callsite = source.Callsite;
            target.Type = source.Type;
            target.Modifier = source.Modifier;
            target.Behaviour = source.Behaviour;
            target.App = new AppModelMapper().Map(source.App);
            target.Fields = source.Fields.Select(x => new FieldModelMapper().Map(x)).ToList();
            target.ReferencedIn = source.ReferencedIn.Select(x => new FieldModelMapper().Map(x)).ToList();
            target.Relations = source.Relations.Select(x => new RelationshipModelMapper().Map(x)).ToList();
            target.IsForeignEntityOf = source.IsForeignEntityOf.Select(x => new RelationshipModelMapper().Map(x)).ToList();
        }

        public EntityViewModel Map(Entity source)
        {
			EntityViewModel target = new();

            Map(source, target);

            return target;
        }
    }
}
