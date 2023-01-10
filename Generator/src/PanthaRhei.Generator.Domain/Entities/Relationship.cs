using System;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Entities
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

    public class RelationshipDto
    {
        public virtual string Key { get; set; }

        public virtual string Entity { get; set; }

        public virtual string Cardinality { get; set; }

        public virtual string WithForeignEntityKey { get; set; }

        public virtual string WithForeignEntity { get; set; }

        public virtual string WithyCardinality { get; set; }

        public virtual bool Required { get; set; }
    }
}
