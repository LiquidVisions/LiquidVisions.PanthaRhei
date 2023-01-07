using System;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Entities
{
    public class ConnectionString
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Definition { get; set; }

        public App App { get; set; }
    }
}
