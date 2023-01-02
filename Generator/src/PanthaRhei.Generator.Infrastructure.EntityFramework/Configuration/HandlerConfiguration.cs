using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Configuration
{
    internal class HandlerConfiguration : IEntityTypeConfiguration<Handler>
    {
        public void Configure(EntityTypeBuilder<Handler> builder)
        {
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired(true);

            builder.Property(x => x.Order)
                .IsRequired(true);

            builder.Property(x => x.SupportedGenerationModes)
                .IsRequired(true)
                .HasConversion(
                    to => to.ToString(),
                    from => (GenerationModes)Enum.Parse(typeof(GenerationModes), from))
                .HasDefaultValue(GenerationModes.Default);
        }
    }
}
