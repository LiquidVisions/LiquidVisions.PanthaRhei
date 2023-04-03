using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.ConnectionStrings;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Mappers.ConnectionStrings
{
    internal class CreateConnectionStringCommandToConnectionStringMapper : IMapper<CreateConnectionStringCommand, ConnectionString>
    {
        public void Map(CreateConnectionStringCommand source, ConnectionString target)
        {
            target.Name = source.Name;
            target.Definition = source.Definition;
            target.App = source.App;
        }

        public ConnectionString Map(CreateConnectionStringCommand source)
        {
            ConnectionString target = new();

            Map(source, target);

            return target;
        }
    }
}
