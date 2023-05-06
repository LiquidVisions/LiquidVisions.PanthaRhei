using LiquidVisions.PanthaRhei.Generated.Application.RequestModels.Expanders;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Mappers.Expanders
{
    internal class UpdateExpanderRequestModelRequestModelMapper : IMapper<UpdateExpanderRequestModel, Expander>
    {
        public void Map(UpdateExpanderRequestModel source, Expander target)
        {
            target.Name = source.Name;
            target.TemplateFolder = source.TemplateFolder;
            target.Order = source.Order;
            target.Apps = source.Apps;
            target.Components = source.Components;
        }

        public Expander Map(UpdateExpanderRequestModel source)
        {
            Expander target = new();

            Map(source, target);

            return target;
        }
    }
}
