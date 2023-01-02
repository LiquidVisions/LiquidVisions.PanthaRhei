using System;
using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Models
{
    public class Entity
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual List<Field> Fields { get; set; }

        public virtual List<Option> Options { get; set; }
    }
}
