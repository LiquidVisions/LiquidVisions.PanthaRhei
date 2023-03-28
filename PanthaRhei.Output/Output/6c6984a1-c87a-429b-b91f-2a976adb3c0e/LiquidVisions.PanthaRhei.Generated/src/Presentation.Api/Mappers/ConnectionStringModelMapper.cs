using LiquidVisions.PanthaRhei.Generated.Client.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using System.Linq;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers
{
    public class ConnectionStringModelMapper : IMapper<ConnectionString, ConnectionStringViewModel>
    {
        public void Map(ConnectionString source, ConnectionStringViewModel target)
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.Definition = source.Definition;
            target.App = new AppModelMapper().Map(source.App);
        }

        public ConnectionStringViewModel Map(ConnectionString source)
        {
			ConnectionStringViewModel target = new();

            Map(source, target);

            return target;
        }
    }
}
