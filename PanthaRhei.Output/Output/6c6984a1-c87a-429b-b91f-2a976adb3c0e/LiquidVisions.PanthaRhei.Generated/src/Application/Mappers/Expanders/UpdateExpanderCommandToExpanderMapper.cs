using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Expanders;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Mappers.Expanders
{
    internal class UpdateExpanderCommandToExpanderMapper : IMapper<UpdateExpanderCommand, Expander>
    {
        public void Map(UpdateExpanderCommand source, Expander target)
        {
            target.Name = source.Name;
            target.TemplateFolder = source.TemplateFolder;
            target.Order = source.Order;
            target.Apps = source.Apps;
            target.Components = source.Components;
        }

        public Expander Map(UpdateExpanderCommand source)
        {
            Expander target = new();

            Map(source, target);

            return target;
        }
    }
}
