using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Configuration
{
    [ExcludeFromCodeCoverage]
    internal class FieldConfiguration : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired(true);

            builder.Property(x => x.Behaviour)
                .HasMaxLength(16)
                .IsRequired(false);

            builder.Property(x => x.Modifier)
                .HasMaxLength(128)
                .IsRequired(true)
                .HasDefaultValue("public");

            builder.HasOne(x => x.Reference)
                .WithMany(x => x.ReferencedIn)
                .IsRequired(false);
        }
    }
}