using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Apps;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Mappers.Apps
{
    internal class CreateAppRequestModelMapper : IMapper<CreateAppRequestModel, App>
    {
        public void Map(CreateAppRequestModel source, App target)
        {
            target.Name = source.Name;
            target.FullName = source.FullName;
            target.Expanders = source.Expanders;
            target.Entities = source.Entities;
            target.ConnectionStrings = source.ConnectionStrings;
        }

        public App Map(CreateAppRequestModel source)
        {
            App target = new();

            Map(source, target);

            return target;
        }
    }
}
