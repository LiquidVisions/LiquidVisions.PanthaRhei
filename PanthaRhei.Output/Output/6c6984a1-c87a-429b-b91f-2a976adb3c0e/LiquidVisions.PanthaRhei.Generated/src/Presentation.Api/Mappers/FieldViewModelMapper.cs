using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using System.Linq;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers
{
    public class FieldViewModelMapper : IMapper<Field, FieldViewModel>
    {
        public void Map(Field source, FieldViewModel target)
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.ReturnType = source.ReturnType;
            target.IsCollection = source.IsCollection;
            target.Modifier = source.Modifier;
            target.GetModifier = source.GetModifier;
            target.SetModifier = source.SetModifier;
            target.Behaviour = source.Behaviour;
            target.Order = source.Order;
            target.Size = source.Size;
            target.Required = source.Required;
            target.Reference = new EntityViewModelMapper().Map(source.Reference);
            target.Entity = new EntityViewModelMapper().Map(source.Entity);
            target.IsKey = source.IsKey;
            target.IsIndex = source.IsIndex;
            target.RelationshipKeys = source.RelationshipKeys.Select(x => new RelationshipViewModelMapper().Map(x)).ToList();
            target.IsForeignEntityKeyOf = source.IsForeignEntityKeyOf.Select(x => new RelationshipViewModelMapper().Map(x)).ToList();
        }

        public FieldViewModel Map(Field source)
        {
			FieldViewModel target = new();

            Map(source, target);

            return target;
        }
    }
}
