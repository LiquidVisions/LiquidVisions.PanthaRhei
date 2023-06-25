using System;
using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Domain.Entities
{
    public class Entity
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Callsite { get; set; }

        public virtual string Type { get; set; }

        public virtual string Modifier { get; set; }

        public virtual string Behaviour { get; set; }

        public virtual App App { get; set; }

        public virtual List<Field> Fields { get; set; } = new List<Field>();

        public virtual List<Field> ReferencedIn { get; set; } = new List<Field>();

        public virtual List<Relationship> Relations { get; set; } = new List<Relationship>();

        public virtual List<Relationship> IsForeignEntityOf { get; set; } = new List<Relationship>();
    }
}
