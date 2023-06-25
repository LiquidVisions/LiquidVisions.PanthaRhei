using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Configuration
{
    [ExcludeFromCodeCoverage]
    public class EntityConfiguration : IEntityTypeConfiguration<Entity>
    {
        public void Configure(EntityTypeBuilder<Entity> builder)
        {
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired(true);

            builder.Property(x => x.Callsite)
                .HasMaxLength(2048)
                .IsRequired(true);

            builder.Property(x => x.Type)
                .HasMaxLength(16)
                .IsRequired(true)
                .HasDefaultValue("class");

            builder.Property(x => x.Behaviour)
                .HasMaxLength(16)
                .IsRequired(false);

            builder.Property(x => x.Modifier)
                .HasMaxLength(128)
                .IsRequired(true)
                .HasDefaultValue("public");

            builder.HasOne(x => x.App)
                .WithMany(x => x.Entities)
                .IsRequired();
        }
    }
}