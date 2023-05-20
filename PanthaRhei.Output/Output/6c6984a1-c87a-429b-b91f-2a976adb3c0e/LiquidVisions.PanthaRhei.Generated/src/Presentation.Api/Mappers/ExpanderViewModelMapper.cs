using LiquidVisions.PanthaRhei.Generated.Presentation.Api.ViewModels;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using System.Linq;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Mappers
{
    public class ExpanderViewModelMapper : IMapper<Expander, ExpanderViewModel>
    {
        public void Map(Expander source, ExpanderViewModel target)
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.TemplateFolder = source.TemplateFolder;
            target.Order = source.Order;
            target.Apps = source?.Apps?.Select(x => new AppViewModelMapper().Map(x)).ToList();
            target.Components = source?.Components?.Select(x => new ComponentViewModelMapper().Map(x)).ToList();
        }

        public ExpanderViewModel Map(Expander source)
        {
			ExpanderViewModel target = new();

            Map(source, target);

            return target;
        }
    }
}
