using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Mappers.Components
{
    internal class UpdateComponentCommandRequestModelMapper : IMapper<UpdateComponentCommand, Component>
    {
        public void Map(UpdateComponentCommand source, Component target)
        {
            target.Name = source.Name;
            target.Description = source.Description;
            target.Packages = source.Packages;
            target.Expander = source.Expander;
        }

        public Component Map(UpdateComponentCommand source)
        {
            Component target = new();

            Map(source, target);

            return target;
        }
    }
}
