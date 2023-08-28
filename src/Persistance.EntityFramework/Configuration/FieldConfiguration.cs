using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Configuration
{
    /// <summary>
    /// Configuration for the <see cref="Field"/> entity.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FieldConfiguration : IEntityTypeConfiguration<Field>
    {
        /// <summary>
        /// Configures the <see cref="Field"/> entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
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

            builder.Property(x => x.Order)
                .IsRequired(true)
                .HasDefaultValue(0);

            builder.Property(x => x.Size)
                .IsRequired(false);

            builder.Property(x => x.Required)
                .IsRequired(true);

            builder.Property(x => x.Modifier)
                .HasMaxLength(128)
                .IsRequired(true)
                .HasDefaultValue("public");

            builder.Property(x => x.IsKey)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x => x.IsIndex)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne(x => x.Reference)
                .WithMany(x => x.ReferencedIn)
                .IsRequired(false);

            builder.HasOne(x => x.Entity)
                .WithMany(x => x.Fields)
                .IsRequired(true);
        }
    }
}
