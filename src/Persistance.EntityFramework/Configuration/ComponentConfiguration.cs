﻿using System;
using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                .WithMany(x => x.Components)
                .IsRequired(false);
        }
    }
}
