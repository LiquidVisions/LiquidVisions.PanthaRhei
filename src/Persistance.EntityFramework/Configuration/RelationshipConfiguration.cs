using System;
using LiquidVisions.PanthaRhei.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Configuration
{
    /// <summary>
    /// Configuration for the <see cref="Relationship"/> entity.
    /// </summary>
    public class RelationshipConfiguration : IEntityTypeConfiguration<Relationship>
    {
        /// <summary>
        /// Configures the <see cref="Relationship"/> entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<Relationship> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Cardinality)
                .HasMaxLength(8)
                .IsRequired(true);

            builder.Property(x => x.WithCardinality)
                .HasMaxLength(8)
                .IsRequired(true);

            builder.Property(x => x.Required)
                .IsRequired(true);

            builder.HasOne(x => x.Entity)
                .WithMany(x => x.Relations)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Key)
                .WithMany(x => x.RelationshipKeys)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.WithForeignEntity)
                .WithMany(x => x.IsForeignEntityOf)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.WithForeignEntityKey)
                .WithMany(x => x.IsForeignEntityKeyOf)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
