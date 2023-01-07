using System;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Entities
{
    public class ConnectionString
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Definition { get; set; }

        public virtual App App { get; set; }
    }
}
