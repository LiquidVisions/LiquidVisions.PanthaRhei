﻿using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Configuration
{
    [ExcludeFromCodeCoverage]
    internal class ComponentConfiguration : IEntityTypeConfiguration<Component>
    {
        public void Configure(EntityTypeBuilder<Component> builder)
        {
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(32);

            builder.Property(x => x.Description)
                .IsRequired(false)
                .HasMaxLength(2056);

            builder.Property(x => x.Version)
                .IsRequired(true)
                .HasMaxLength(9);

            builder.HasMany(x => x.Packages)
                .WithOne(x => x.Component)
                .IsRequired(false);
        }
    }
}