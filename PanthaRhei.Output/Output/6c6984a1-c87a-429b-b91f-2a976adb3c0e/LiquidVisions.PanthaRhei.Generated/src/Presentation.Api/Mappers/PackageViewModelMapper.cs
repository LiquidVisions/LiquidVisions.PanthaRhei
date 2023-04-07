using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using System.Linq;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers
{
    public class PackageViewModelMapper : IMapper<Package, PackageViewModel>
    {
        public void Map(Package source, PackageViewModel target)
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.Version = source.Version;
            target.Component = new ComponentViewModelMapper().Map(source.Component);
        }

        public PackageViewModel Map(Package source)
        {
			PackageViewModel target = new();

            Map(source, target);

            return target;
        }
    }
}
