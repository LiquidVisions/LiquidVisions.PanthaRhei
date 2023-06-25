using System;

namespace LiquidVisions.PanthaRhei.Domain.Entities
{
    public class Relationship
    {
        public virtual Guid Id { get; set; }

        public virtual Field Key { get; set; }

        public virtual Entity Entity { get; set; }

        public virtual string Cardinality { get; set; }

        public virtual Field WithForeignEntityKey { get; set; }

        public virtual Entity WithForeignEntity { get; set; }

        public virtual string WithCardinality { get; set; }

        public virtual bool Required { get; set; }
    }
}
