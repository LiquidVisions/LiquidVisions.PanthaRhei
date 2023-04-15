using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Mappers.Components
{
    internal class UpdateComponentRequestModelRequestModelMapper : IMapper<UpdateComponentRequestModel, Component>
    {
        public void Map(UpdateComponentRequestModel source, Component target)
        {
            target.Name = source.Name;
            target.Description = source.Description;
            target.Packages = source.Packages;
            target.Expander = source.Expander;
        }

        public Component Map(UpdateComponentRequestModel source)
        {
            Component target = new();

            Map(source, target);

            return target;
        }
    }
}
