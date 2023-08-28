using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Configuration
{
    /// <summary>
    /// Represents the configuration for the <see cref="App"/> entity.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AppConfiguration : IEntityTypeConfiguration<App>
    {
        /// <summary>
        /// Configures the <see cref="App"/> entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<App> builder)
        {
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired(true);

            builder.Property(x => x.FullName)
                .HasMaxLength(2048)
                .IsRequired(true);
        }
    }
}
