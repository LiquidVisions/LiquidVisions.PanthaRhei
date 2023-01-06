using System;
using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Models
{
    public class Entity
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Type { get; set; }

        public virtual string Modifier { get; set; }

        public virtual string Behaviour { get; set; }

        public virtual App App { get; set; }

        public virtual List<Field> Fields { get; set; } = new List<Field>();

        public virtual List<Field> ReferencedIn { get; set; } = new List<Field>();

        public decimal Test1 { get; set; }

        public bool test2 { get; set; }

        public GenerationModes Mode { get; set; }
    }
}
