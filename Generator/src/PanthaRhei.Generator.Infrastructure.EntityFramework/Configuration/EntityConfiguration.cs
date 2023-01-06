using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Configuration
{
    [ExcludeFromCodeCoverage]
    internal class EntityConfiguration : IEntityTypeConfiguration<Entity>
    {
        public void Configure(EntityTypeBuilder<Entity> builder)
        {
            builder.HasKey(x => new { x.Id });

            builder.Ignore(x => x.Test1);
            builder.Ignore(x => x.test2);
            builder.Ignore(x => x.Mode);

            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Name)
                .HasMaxLength(128)
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

            builder.HasMany(x => x.Fields)
                .WithOne(x => x.Entity)
                .IsRequired(true);

            builder.HasOne(x => x.App)
                .WithMany(x => x.Entities)
                .IsRequired();
        }
    }
}