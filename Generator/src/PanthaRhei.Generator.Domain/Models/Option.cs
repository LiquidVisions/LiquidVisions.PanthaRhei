using System;
using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Models
{
    public class Option
    {
        public virtual Guid Id { get; set; }

        public virtual string Key { get; set; }

        public virtual string Value { get; set; }

        public virtual List<Field> Fields { get; set; }

        public virtual List<Entity> Entities { get; set; }
    }
}
