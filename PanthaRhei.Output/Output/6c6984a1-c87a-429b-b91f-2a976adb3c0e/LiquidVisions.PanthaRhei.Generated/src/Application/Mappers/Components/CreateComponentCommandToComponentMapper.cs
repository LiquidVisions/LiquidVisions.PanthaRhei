using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Mappers.Components
{
    internal class CreateComponentCommandToComponentMapper : IMapper<CreateComponentCommand, Component>
    {
        public void Map(CreateComponentCommand source, Component target)
        {
            target.Name = source.Name;
            target.Description = source.Description;
            target.Packages = source.Packages;
            target.Expander = source.Expander;
        }

        public Component Map(CreateComponentCommand source)
        {
            Component target = new();

            Map(source, target);

            return target;
        }
    }
}
