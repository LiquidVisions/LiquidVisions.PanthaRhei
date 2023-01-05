using System;
using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Configuration
{
    [ExcludeFromCodeCoverage]
    internal class OptionConfiguration : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.HasKey(x => new { x.Id, x.Key });

            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Key)
                .HasMaxLength(64)
                .IsRequired(true);

            builder.Property(x => x.Value)
                .HasMaxLength(64)
                .IsRequired(true);
        }
    }
}