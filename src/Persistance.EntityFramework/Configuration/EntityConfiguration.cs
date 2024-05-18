using System;
using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Configuration
{
    /// <summary>
    /// Configuration for the <see cref="Entity"/> entity.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class EntityConfiguration : IEntityTypeConfiguration<Entity>
    {
        /// <summary>
        /// Configures the <see cref="Entity"/> entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<Entity> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired(true);

            builder.Property(x => x.CallSite)
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
