using System;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Models
{
    public class Handler
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual int Order { get; set; }

        public virtual Expander Expander { get; set; }

        public virtual GenerationModes SupportedGenerationModes { get; set; } = GenerationModes.Default;
    }
}
