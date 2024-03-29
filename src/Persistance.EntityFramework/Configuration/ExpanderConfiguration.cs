﻿using System;
using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Configuration
{
    /// <summary>
    /// Configuration for the <see cref="Expander"/> entity.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ExpanderConfiguration : IEntityTypeConfiguration<Expander>
    {
        /// <summary>
        /// Configures the <see cref="Expander"/> entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<Expander> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Enabled)
                .IsRequired(true);
        }
    }
}
