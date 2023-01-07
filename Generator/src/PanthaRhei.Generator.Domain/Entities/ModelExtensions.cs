using System.IO;
using System.Linq;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Entities
{
    public static class ModelExtensions
    {
        public static Component GetComponentByName(this Expander expander, string name)
        {
            return expander.Components.Single(x => x.Name == name);
        }

    }
}
