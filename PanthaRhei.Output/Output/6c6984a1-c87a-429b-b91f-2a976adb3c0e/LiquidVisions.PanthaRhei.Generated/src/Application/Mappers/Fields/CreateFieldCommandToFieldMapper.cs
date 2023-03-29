using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Fields;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Mappers.Fields
{
    internal class CreateFieldCommandToFieldMapper : IMapper<CreateFieldCommand, Field>
    {
        public void Map(CreateFieldCommand source, Field target)
        {
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
            target.Reference = source.Reference;
            target.Entity = source.Entity;
            target.IsKey = source.IsKey;
            target.IsIndex = source.IsIndex;
            target.RelationshipKeys = source.RelationshipKeys;
            target.IsForeignEntityKeyOf = source.IsForeignEntityKeyOf;
        }

        public Field Map(CreateFieldCommand source)
        {
            Field target = new();

            Map(source, target);

            return target;
        }
    }
}
