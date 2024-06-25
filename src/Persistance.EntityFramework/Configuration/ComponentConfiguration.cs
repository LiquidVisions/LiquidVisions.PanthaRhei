using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using LiquidVisions.PanthaRhei.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Configuration
{

    /// <summary>
    /// Configuration for the <see cref="Component"/> entity.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ComponentConfiguration : IEntityTypeConfiguration<Component>
    {
        /// <summary>
        /// Configures the <see cref="Component"/> entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<Component> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(32);

            builder.Property(x => x.Description)
                .IsRequired(false)
                .HasMaxLength(2056);

            builder.HasOne(x => x.Expander)
                .WithOne(x => x.Component)
                .HasForeignKey<Expander>(x => x.Id);
        }
    }
}
