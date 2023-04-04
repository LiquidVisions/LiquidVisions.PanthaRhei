using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using System.Linq;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers
{
    public class ComponentViewModelMapper : IMapper<Component, ComponentViewModel>
    {
        public void Map(Component source, ComponentViewModel target)
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Packages = source.Packages.Select(x => new PackageModelMapper().Map(x)).ToList();
            target.Expander = new ExpanderModelMapper().Map(source.Expander);
        }

        public ComponentViewModel Map(Component source)
        {
			ComponentViewModel target = new();

            Map(source, target);

            return target;
        }
    }
}
