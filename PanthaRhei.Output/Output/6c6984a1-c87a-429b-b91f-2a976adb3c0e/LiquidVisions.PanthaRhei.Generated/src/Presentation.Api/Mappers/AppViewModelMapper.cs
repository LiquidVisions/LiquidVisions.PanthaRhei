using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using System.Linq;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers
{
    public class AppViewModelMapper : IMapper<App, AppViewModel>
    {
        public void Map(App source, AppViewModel target)
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.FullName = source.FullName;
            target.Expanders = source.Expanders.Select(x => new ExpanderViewModelMapper().Map(x)).ToList();
            target.Entities = source.Entities.Select(x => new EntityViewModelMapper().Map(x)).ToList();
            target.ConnectionStrings = source.ConnectionStrings.Select(x => new ConnectionStringViewModelMapper().Map(x)).ToList();
        }

        public AppViewModel Map(App source)
        {
			AppViewModel target = new();

            Map(source, target);

            return target;
        }
    }
}
