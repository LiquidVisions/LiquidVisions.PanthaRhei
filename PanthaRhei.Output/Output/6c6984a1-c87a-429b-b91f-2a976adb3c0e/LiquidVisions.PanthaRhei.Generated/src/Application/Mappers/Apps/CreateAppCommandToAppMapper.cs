using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Apps;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Mappers.Apps
{
    internal class CreateAppCommandToAppMapper : IMapper<CreateAppCommand, App>
    {
        public void Map(CreateAppCommand source, App target)
        {
            target.Name = source.Name;
            target.FullName = source.FullName;
            target.Expanders = source.Expanders;
            target.Entities = source.Entities;
            target.ConnectionStrings = source.ConnectionStrings;
        }

        public App Map(CreateAppCommand source)
        {
            App target = new();

            Map(source, target);

            return target;
        }
    }
}
