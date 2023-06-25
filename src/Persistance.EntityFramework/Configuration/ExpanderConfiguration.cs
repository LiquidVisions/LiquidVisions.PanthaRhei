using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Configuration
{
    [ExcludeFromCodeCoverage]
    public class ExpanderConfiguration : IEntityTypeConfiguration<Expander>
    {
        public void Configure(EntityTypeBuilder<Expander> builder)
        {
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Id)
                .IsRequired(true);
        }
    }
}