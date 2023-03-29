using LiquidVisions.PanthaRhei.Generated.Client.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using System.Linq;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers
{
    public class ExpanderModelMapper : IMapper<Expander, ExpanderViewModel>
    {
        public void Map(Expander source, ExpanderViewModel target)
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.TemplateFolder = source.TemplateFolder;
            target.Order = source.Order;
            target.Apps = source.Apps.Select(x => new AppModelMapper().Map(x)).ToList();
            target.Components = source.Components.Select(x => new ComponentModelMapper().Map(x)).ToList();
        }

        public ExpanderViewModel Map(Expander source)
        {
			ExpanderViewModel target = new();

            Map(source, target);

            return target;
        }
    }
}
