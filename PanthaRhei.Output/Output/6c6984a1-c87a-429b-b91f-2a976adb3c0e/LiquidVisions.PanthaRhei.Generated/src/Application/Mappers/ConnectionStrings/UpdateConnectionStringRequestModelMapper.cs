using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.ConnectionStrings;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Mappers.ConnectionStrings
{
    internal class UpdateConnectionStringRequestModelMapper : IMapper<UpdateConnectionStringRequestModel, ConnectionString>
    {
        public void Map(UpdateConnectionStringRequestModel source, ConnectionString target)
        {
            target.Name = source.Name;
            target.Definition = source.Definition;
            target.App = source.App;
        }

        public ConnectionString Map(UpdateConnectionStringRequestModel source)
        {
            ConnectionString target = new();

            Map(source, target);

            return target;
        }
    }
}
