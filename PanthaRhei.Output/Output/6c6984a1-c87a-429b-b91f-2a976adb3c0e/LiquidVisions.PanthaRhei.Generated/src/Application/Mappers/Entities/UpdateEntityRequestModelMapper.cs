using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Entities;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Mappers.Entities
{
    internal class UpdateEntityCommandRequestModelMapper : IMapper<UpdateEntityCommand, Entity>
    {
        public void Map(UpdateEntityCommand source, Entity target)
        {
            target.Name = source.Name;
            target.Callsite = source.Callsite;
            target.Type = source.Type;
            target.Modifier = source.Modifier;
            target.Behaviour = source.Behaviour;
            target.App = source.App;
            target.Fields = source.Fields;
            target.ReferencedIn = source.ReferencedIn;
            target.Relations = source.Relations;
            target.IsForeignEntityOf = source.IsForeignEntityOf;
        }

        public Entity Map(UpdateEntityCommand source)
        {
            Entity target = new();

            Map(source, target);

            return target;
        }
    }
}
