using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Components;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Mappers.Components
{
    internal class CreateComponentRequestModelRequestModelMapper : IMapper<CreateComponentRequestModel, Component>
    {
        public void Map(CreateComponentRequestModel source, Component target)
        {
            target.Name = source.Name;
            target.Description = source.Description;
            target.Packages = source.Packages;
            target.Expander = source.Expander;
        }

        public Component Map(CreateComponentRequestModel source)
        {
            Component target = new();

            Map(source, target);

            return target;
        }
    }
}
