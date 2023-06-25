namespace LiquidVisions.PanthaRhei.Domain.Models
{
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
