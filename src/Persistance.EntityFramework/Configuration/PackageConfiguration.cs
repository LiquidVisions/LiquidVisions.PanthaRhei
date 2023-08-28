using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Configuration
{
    /// <summary>
    /// Configuration for the <see cref="Package"/> entity.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        /// <summary>
        /// Configures the <see cref="Package"/> entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(256);

            builder.HasOne(x => x.Component)
                .WithMany(x => x.Packages)
                .IsRequired(false);
        }
    }
}
