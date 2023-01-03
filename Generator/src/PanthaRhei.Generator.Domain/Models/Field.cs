using System;
using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Models
{
    public class Field
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual DataType DataType { get; set; }

        public virtual Entity Entity { get; set; }

        public virtual List<Option> Options { get; set; }
    }
}
