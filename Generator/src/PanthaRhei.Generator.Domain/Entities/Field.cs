using System;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Entities
{
    public class Field
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string ReturnType { get; set; }

        public virtual bool IsCollection { get; set; }

        public virtual string Modifier { get; set; }

        public virtual string GetModifier { get; set; }

        public virtual string SetModifier { get; set; }

        public virtual string Behaviour { get; set; }

        public virtual Entity Reference { get; set; }

        public virtual Entity Entity { get; set; }
    }
}
