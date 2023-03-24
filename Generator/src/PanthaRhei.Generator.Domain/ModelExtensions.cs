using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generator.Domain
{
    public static class ModelExtensions
    {
        public static Component GetComponentByName(this Expander expander, string name)
        {
            return expander.Components.Single(x => x.Name == name);
        }
    }
}
