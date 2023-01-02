using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Models
{
    public class DataType
    {
        public virtual string Name { get; set; }

        public virtual List<Field> Fields { get; set; }
    }
}
